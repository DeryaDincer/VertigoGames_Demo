using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertigoGames.Interfaces;
using VertigoGames.Utility;

namespace VertigoGames.Pooling
{
    public sealed class ObjectPoolManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform _poolParent;
        [SerializeField] private ObjectPoolInfo[] _pools;

        #region Initialization and Deinitialization
        public void Initialize()
        {
            foreach (var pool in _pools)
            {
                pool.InitializePool(_poolParent);
            }
        }
        
        public void Deinitialize() { }
        #endregion

        public T GetObjectFromPool<T>(Transform parent = null, Vector3? localScale = null) where T : PoolObject
        {
            var pool = _pools.FirstOrDefault(p => p.ObjectPrefab is T);
            if (pool == null)
                throw new System.InvalidOperationException($"No pool found for type {typeof(T)}");

            T pooledObject = pool.PooledObjects.Count == 0
                ? CreateNewPooledObject<T>(pool)
                : GetExistingPooledObject<T>(pool);

            if (parent != null)
                pooledObject.transform.SetParent(parent);

            if (localScale.HasValue)
                pooledObject.transform.localScale = localScale.Value;

            return pooledObject;
        }
        
        private T CreateNewPooledObject<T>(ObjectPoolInfo pool) where T : PoolObject
        {
            return pool.InstantiatePoolObject(pool.ObjectPrefab, _poolParent) as T;
        }

        private T GetExistingPooledObject<T>(ObjectPoolInfo pool) where T : PoolObject
        {
            T pooledObject = pool.PooledObjects.Dequeue() as T;
            pooledObject.OnSpawn();
            pooledObject.Show();
            return pooledObject;
        }
        
        public void ReturnToPool(PoolObject poolObject)
        {
            var pool = _pools.FirstOrDefault(p => p.ObjectPrefab.GetType() == poolObject.GetType());
            
            if (pool != null)
            {
                poolObject.OnDeactivate();
                poolObject.gameObject.SetActive(false);
                pool.PooledObjects.Enqueue(poolObject);
            }
            else
            {
                Debug.LogWarning($"No pool found for {poolObject.GetType()}, destroying object.");
                Destroy(poolObject.gameObject);
            }
        }
        
        [System.Serializable]
        private class ObjectPoolInfo
        {
            private Queue<PoolObject> _pooledObjects = new();
            [SerializeField] private PoolObject _objectPrefab;
            [SerializeField] private int _poolSize;

            public PoolObject ObjectPrefab => _objectPrefab;
            public Queue<PoolObject> PooledObjects => _pooledObjects;

            public void InitializePool(Transform parent)
            {
                for (int i = 0; i < _poolSize; i++)
                {
                    _pooledObjects.Enqueue(InstantiatePoolObject(_objectPrefab, parent));
                }
            }
            
            public PoolObject InstantiatePoolObject(PoolObject prefab, Transform parent)
            {
                PoolObject poolObject = Instantiate(prefab, parent);
                poolObject.OnCreated();
                return poolObject;
            }
        }
    }
}
