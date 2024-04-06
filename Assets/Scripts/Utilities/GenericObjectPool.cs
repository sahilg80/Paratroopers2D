using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    /// <summary>
    /// This is a Generic Object Pool Class with basic functionality, which can be inherited to implement object pools for any type of objects.
    /// </summary>
    /// <typeparam object Type to be pooled = "T"></typeparam>
    public class GenericObjectPool<T> where T : class
    {
        //protected List<T> inactivePooledItems = new List<T>();
        
        //public T GetItemFromPool(T component)
        //{
            
        //    if (inactivePooledItems == null)
        //    {
        //        inactivePooledItems = new List<T>();
        //    }

        //    T item;

        //    if (inactivePooledItems.Count > 0)
        //    {
        //        item = inactivePooledItems[0];
        //        inactivePooledItems.RemoveAt(0);
        //        return item;
        //    }
        //    else
        //    {
        //        item = UnityEngine.Object.Instantiate(component);
        //    }

        //    return item;

        //}

        //public void ReturnItemToPool(T component)
        //{
        //    if (inactivePooledItems == null)
        //    {
        //        Debug.Log("pool does not exist");
        //        return;
        //    }
        //    ResetObject(component);

        //    inactivePooledItems.Add(component);

        //}

        //private void ResetObject(T objectToDespawn)
        //{
        //    objectToDespawn.transform.position = Vector3.zero;
        //    objectToDespawn.transform.rotation = Quaternion.identity;
        //    objectToDespawn.gameObject.SetActive(false);
        //}

        public List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        protected T GetItem()
        {
            if (pooledItems.Count > 0)
            {
                PooledItem<T> item = pooledItems.Find(item => !item.IsInUse);
                if (item != null)
                {
                    item.IsInUse = true;
                    return item.Item;
                }
            }
            return CreateNewPooledItem();
        }

        protected virtual T CreateItem()
        {
            throw new NotImplementedException("CreateItem() method not implemented in derived class");
        }

        protected void ReturnItem(T item)
        {
            PooledItem<T> pooledItem = pooledItems.Find(i => i.Item.Equals(item));
            pooledItem.IsInUse = false;
        }

        private T CreateNewPooledItem()
        {
            PooledItem<T> newItem = new PooledItem<T>();
            newItem.Item = CreateItem();
            newItem.IsInUse = true;
            pooledItems.Add(newItem);
            return newItem.Item;
        }


        public class PooledItem<U>
        {
            public U Item;
            public bool IsInUse;
        }

    }

}