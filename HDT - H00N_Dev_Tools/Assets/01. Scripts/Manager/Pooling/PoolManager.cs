using System.Collections.Generic;
using UnityEngine;

namespace H00N.Manager
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager instance = null;
        public static PoolManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<PoolManager>();

                return instance;
            }
        }

        [SerializeField] Transform pooler = null;
        [SerializeField] List<PoolableMono> poolingList = new List<PoolableMono>();
        private Dictionary<string, Pool<PoolableMono>> pools = new Dictionary<string, Pool<PoolableMono>>();

        private void Awake()
        {
            foreach (PoolableMono p in poolingList)
                CreatePool(p);
        }

        private void CreatePool(PoolableMono prefab)
        {
            if (pools.ContainsKey(prefab.name))
            {
                Debug.LogWarning($"[{prefab.name}] same name of poolable object already existed in pools, returning");
                return;
            }

            Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, pooler);
            pools.Add(prefab.name, pool);
        }

        public PoolableMono Pop(string prefabName)
        {
            if (!pools.ContainsKey(prefabName))
            {
                Debug.LogWarning($"[{prefabName}] current name of poolable object doesn't exist in pools, returning null");
                return null;
            }

            PoolableMono returnValue = pools[prefabName].Pop();
            returnValue.transform.SetParent(null);
            returnValue.Reset();

            return returnValue;
        }

        public PoolableMono Pop(PoolableMono prefab)
        {
            if (!pools.ContainsKey(prefab.name))
            {
                Debug.LogWarning($"[{prefab.name}] current name of poolable object doesn't exist in pools, returning null");
                return null;
            }

            PoolableMono returnValue = pools[prefab.name].Pop();
            returnValue.transform.SetParent(null);
            returnValue.Reset();

            return returnValue;
        }

        public void Push(PoolableMono obj)
        {
            if (!pools.ContainsKey(obj.name))
            {
                Debug.LogWarning($"[{obj.name}] current name if poolable object doesn't exist in pools, destroy object");
                Destroy(obj.gameObject);
                return;
            }

            obj.transform.SetParent(pooler);
            pools[obj.name].Push(obj);
        }
    }
}
