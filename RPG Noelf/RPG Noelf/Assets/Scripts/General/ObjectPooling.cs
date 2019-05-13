using System;
using System.Collections.Generic;

namespace RPG_Noelf.Assets.Scripts.General
{
    class ObjectPooling<T>
    {
        private Queue<T> Pool;
        public List<T> Pooled { get; set; }

        public int PoolSize
        {
            get {
                return Pool.Count;
            }
        }

        public ObjectPooling()
        {
            Pool = new Queue<T>();
            Pooled = new List<T>();
        }

        public void AddToPool(T poolObject)
        {
            Pool.Enqueue(poolObject);
        }
        
        public void GetFromPool(out T pooledObject)
        {
            if(Pool != null)
            {
                pooledObject = Pool.Dequeue();
                Pooled.Add(pooledObject);
            } else
            {
                pooledObject = default(T);
            }
        }

        public void ReturnToPool()
        {
            foreach(T obj in Pooled)
            {
                Pool.Enqueue(obj);
            }
            Pooled.Clear();
        }

    }
}
