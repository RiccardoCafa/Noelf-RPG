using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.General
{
    class ObjectPooling<T>
    {
        private Dictionary<string, Queue<T>> Pool;

        public ObjectPooling()
        {
            Pool = new Dictionary<string, Queue<T>>();
        }

        public void CreatePool(string poolName)
        {
            Pool.Add(poolName, new Queue<T>());
        }

        public void AddToPool(string poolName, T pooledObject)
        {
            Pool[poolName].Enqueue(pooledObject);
        }

        public int GetPoolSize(string poolName)
        {
            return Pool[poolName].Count;
        }

        public bool ExistPool(string poolName)
        {
            return Pool.ContainsKey(poolName);
        }

        public void GetFromPool(string poolName, out T poolObject)
        {
            if(Pool.ContainsKey(poolName))
            {
                poolObject = Pool[poolName].Dequeue();
            } else
            {
                poolObject = default(T);
            }
        }

    }
}
