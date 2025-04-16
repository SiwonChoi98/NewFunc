using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<PoolObjectType, Queue<BasePoolObject>> PoolDictionary = new Dictionary<PoolObjectType, Queue<BasePoolObject>>();
    
    //public-----------------------------------------------------------------------


    //생성
    
    //GameObject
    public BasePoolObject SpawnGameObject(PoolObjectType poolObjectType, BasePoolObject basePoolObject, Vector3 pos,  Quaternion rotaion)
    {
        BasePoolObject poolObject = SpawnFromPool(poolObjectType, basePoolObject, pos, rotaion);
        poolObject.SetPoolObjectType(poolObjectType);

        return poolObject;
    }

    //UI
    public BasePoolObject SpawnUI(PoolObjectType poolObjectType, BasePoolObject basePoolObject, Transform transformUI)
    {
        BasePoolObject poolObject = SpawnFromPool(poolObjectType, basePoolObject, transformUI);
        poolObject.SetPoolObjectType(poolObjectType);

        return poolObject;
    }
    
    //GameObject
    private BasePoolObject SpawnFromPool(PoolObjectType poolObjectType, BasePoolObject poolObject, Vector3 position, Quaternion rotation)
    {
        if (PoolDictionary.TryGetValue(poolObjectType, out Queue<BasePoolObject> queue))
        {
            if (queue.Count > 0)
            {
                BasePoolObject poolObj = DequeuePoolObject(poolObjectType);
                
                poolObj.gameObject.SetActive(true);
                poolObj.gameObject.transform.SetPositionAndRotation(position, rotation);
                
                return poolObj;
            }
        }
        
        return CreatePoolObject(poolObject, position, rotation);  
    }

    //UI
    private BasePoolObject SpawnFromPool(PoolObjectType poolObjectType, BasePoolObject poolObject, Transform spawnTransform)
    {
        if (PoolDictionary.TryGetValue(poolObjectType, out Queue<BasePoolObject> queue))
        {
            if (queue.Count > 0)
            {
                BasePoolObject poolObj = DequeuePoolObject(poolObjectType);
                
                poolObj.transform.SetParent(spawnTransform);
                poolObj.transform.position = spawnTransform.position;
                poolObj.gameObject.SetActive(true);
                
                return poolObj;
            }
        }
        
        return CreatePoolObject(poolObject, spawnTransform);
    }
    
    //해제
    public void ReturnToPool(PoolObjectType poolObjectType, BasePoolObject poolObject)
    {
        poolObject.transform.SetParent(null);
        poolObject.gameObject.SetActive(false);
        
        if (PoolDictionary.ContainsKey(poolObjectType))
        {
            EnqueuePoolObject(poolObjectType, poolObject);
        }
        else
        {
            // 키가 없는 경우 새로운 큐를 만들어 추가
            PoolDictionary[poolObjectType] = new Queue<BasePoolObject>();
            EnqueuePoolObject(poolObjectType, poolObject);
        }
    }

    //해제 위치 지정
    public void ReturnToPool(PoolObjectType poolObjectType, BasePoolObject poolObject, Transform parent)
    {
        poolObject.transform.SetParent(parent);
        poolObject.gameObject.SetActive(false);
        
        if (PoolDictionary.ContainsKey(poolObjectType))
        {
            EnqueuePoolObject(poolObjectType, poolObject);
        }
        else
        {
            // 키가 없는 경우 새로운 큐를 만들어 추가
            PoolDictionary[poolObjectType] = new Queue<BasePoolObject>();
            EnqueuePoolObject(poolObjectType, poolObject);
        }
    }
    
    //private---------------------------------------------------
    
    private BasePoolObject CreatePoolObject(BasePoolObject poolObject, Vector3 pos, Quaternion rotation)
    {
        BasePoolObject obj = Instantiate(poolObject, pos, rotation);
        return obj;
    }
    
    private BasePoolObject CreatePoolObject(BasePoolObject poolObject, Transform spawnTransform)
    {
        BasePoolObject obj = Instantiate(poolObject, spawnTransform);
        return obj;
    }
    
    private BasePoolObject DequeuePoolObject(PoolObjectType poolObjectType)
    {
        BasePoolObject obj = PoolDictionary[poolObjectType].Dequeue();
        return obj;
    }
    
    private void EnqueuePoolObject(PoolObjectType poolObjectType, BasePoolObject poolObject)
    {
        PoolDictionary[poolObjectType].Enqueue(poolObject);
    }

}
