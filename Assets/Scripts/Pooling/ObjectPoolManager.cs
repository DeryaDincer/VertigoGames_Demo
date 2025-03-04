using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertigoGames.Interfaces;
using VertigoGames.Utility;

namespace VertigoGames.Pooling
{
    public sealed class ObjectPoolManager : MonoBehaviour,IInitializable
    {
        public static ObjectPoolManager Instance { get; private set; }
        [SerializeField] private Transform _poolParent;
        [SerializeField] private ObjectPoolInfo[] _pools;

        public ObjectPoolManager()
        {
            if (Instance != null)
            {
                Debug.Log("You are trying to create another instance of TASK HANDLER. Ignoring...");
                return;
            }

            Instance = this;
        }
        
        #region Initialization and Deinitialization
        public void Initialize() => InitializeAllPools();
        
        public void Deinitialize() { }
        
        #endregion

        private void InitializeAllPools()
        {
            _pools.ToList().ForEach(pool => pool.InitializePool(_poolParent));
        }

        public T GetObjectFromPool<T>(Transform parent = null, Vector3? localScale = null) where T : PoolObject
        {
            foreach (var pool in _pools)
            {
                if (pool.ObjectPrefab is T)
                {
                    T pooledObject = pool.PooledObjects.Count == 0
                        ? CreateNewPooledObject<T>(pool)
                        : GetExistingPooledObject<T>(pool);

                    if (parent != null)
                    {
                        pooledObject.transform.SetParent(parent);
                    }

                    if (localScale.HasValue)
                    {
                        pooledObject.transform.localScale = localScale.Value;
                    }

                    return pooledObject;
                }
            }

            throw new System.InvalidOperationException($"No pool found for type {typeof(T)}");
        }
        
        public T GetObjectFromPoolOld<T>() where T : PoolObject
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
            pooledObject.Show();
            return pooledObject;
        }
        
        public void ReturnToPool(PoolObject poolObject)
        {
            foreach (var pool in _pools)
            {
                if (pool.ObjectPrefab.GetType() == poolObject.GetType())
                {
                    poolObject.OnDeactivate(); 
                    poolObject.gameObject.SetActive(false);

                    pool.PooledObjects.Enqueue(poolObject);
                    return;
                }
            }

            Debug.LogWarning($"No pool found for object of type {poolObject.GetType()}. Destroying object instead.");
            Destroy(poolObject.gameObject); // Eğer pool bulunamazsa, nesneyi yok et
        }
        

        [System.Serializable]
        private class ObjectPoolInfo
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