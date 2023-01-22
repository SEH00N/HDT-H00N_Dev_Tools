using System.Collections.Generic;
using UnityEngine;

namespace H00N.Manager
{
    public class Pool<T> where T : MonoBehaviour
    {
        private Stack<T> pool = new Stack<T>();
        private Transform parent = null;
        private T prefab = null;

        public Pool(T prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }

        public T Pop()
        {
            T returnValue = null;

            if (pool.Count > 0)
            {
                returnValue = pool.Pop();
                returnValue.gameObject.SetActive(true);
            }
            else
            {
                returnValue = GameObject.Instantiate(prefab, parent);
                returnValue.gameObject.name = returnValue.gameObject.name.Replace("(Clone)", "");
            }

            return returnValue;
        }

        public void Push(T obj)
        {
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }
}
