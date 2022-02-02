using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectPooling
{
    public class PoolTask 
    {
        private readonly List<IPoolable> _freeObjects;
        private readonly Transform _container;

        public PoolTask(Transform container)
        {
            _freeObjects = new List<IPoolable>();
            _container = container;
        }

        public T GetFreeObject<T>(T _prefab) where T : MonoBehaviour, IPoolable
        {
            T poolObject = null;
            if (_freeObjects.Count > 0)
            {
                poolObject = _freeObjects.Last() as T;
                poolObject.GameObject.SetActive(true);
                _freeObjects.Remove(poolObject);
            }
            
            poolObject  ??= Object.Instantiate(_prefab);
            poolObject.OnReturnToPool += ReturnToPool;
            return poolObject;
        }

        private void ReturnToPool(IPoolable poolObject)
        {
            _freeObjects.Add(poolObject);
            poolObject.GameObject.SetActive(false);
            poolObject.Transform.SetParent(_container);
            poolObject.OnReturnToPool -= ReturnToPool;
        }
    }
}