using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool
    {
        private readonly Dictionary<IPoolable,PoolTask> _activePoolTasks;
        public readonly Transform _objectPoolTransform;

        private static ObjectPool instance;
        public static ObjectPool Instance => instance ??= new ObjectPool();
        
        private ObjectPool()
        {
            _activePoolTasks = new Dictionary<IPoolable, PoolTask>();
            _objectPoolTransform = new GameObject().transform;
            _objectPoolTransform.name = "ObjectPool";
        }

        public T GetObject<T>(T prefab)  where T : MonoBehaviour, IPoolable
        {
            if (!_activePoolTasks.TryGetValue(prefab, out var poolTask))
            {
                AddTaskToPool(prefab, out poolTask);
            }

            return poolTask.GetFreeObject<T>(prefab);
        }

        private void AddTaskToPool<T>(T prefab, out PoolTask poolTask) where T : MonoBehaviour, IPoolable
        {
            GameObject container = new GameObject
            {
                name = $"{prefab.name}s_pool"
            };
            container.transform.SetParent(_objectPoolTransform);
            poolTask = new PoolTask(container.transform);
            _activePoolTasks.Add(prefab, poolTask);
        }
    }
}