using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertigoGames.Interfaces;
using VertigoGames.Utility;

namespace VertigoGames.Pooling
{
    public sealed class ObjectPoolManager : Singleton<ObjectPoolManager>, IInitializable
    {
        [SerializeField] private Transform _poolParent;
        [SerializeField] private ObjectPoolInfo[] _pools;

        public void Initialize() => InitializeAllPools();
        
        public void Deinitialize() { }

        private void InitializeAllPools()
        {
            _pools.ToList().ForEach(pool => pool.InitializePool(_poolParent));
        }

        public T GetObjectFromPool<T>() where T : PoolObject
        {
            foreach (var pool in _pools)
            {
                if (pool.ObjectPrefab is T)
                {
                    return pool.PooledObjects.Count == 0
                        ? CreateNewPooledObject<T>(pool)
                        : GetExistingPooledObject<T>(pool);
                }
            }

            throw new System.InvalidOperationException($"No pool found for type {typeof(T)}");
        }

        private T CreateNewPooledObject<T>(ObjectPoolInfo pool) where T : PoolObject
        {
            PoolObject newObject = Instantiate(pool.ObjectPrefab, _poolParent);
            newObject.OnCreated();
            return newObject as T;
        }

        private T GetExistingPooledObject<T>(ObjectPoolInfo pool) where T : PoolObject
        {
            T pooledObject = pool.PooledObjects.Dequeue() as T;
            pooledObject.OnSpawn();
            return pooledObject;
        }

        [System.Serializable]
        private struct ObjectPoolInfo
        {
            private Queue<PoolObject> _pooledObjects;
            [SerializeField] private PoolObject _objectPrefab;
            [SerializeField] private int _poolSize;

            public PoolObject ObjectPrefab => _objectPrefab;
            public Queue<PoolObject> PooledObjects => _pooledObjects;

            public void InitializePool(Transform parent)
            {
                _pooledObjects = new Queue<PoolObject>();

                for (int i = 0; i < _poolSize; i++)
                {
                    PoolObject poolObject = Instantiate(_objectPrefab, parent);
                    poolObject.OnCreated();
                    _pooledObjects.Enqueue(poolObject);
                }
            }
        }
    }
}