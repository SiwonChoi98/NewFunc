using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BasePoolObject : MonoBehaviour
{
    private PoolObjectType _poolObjectType;
    private string _assetAddress;
    public void SetPoolObjectType(PoolObjectType poolObjectType){
        _poolObjectType = poolObjectType;
    }

    public void SetAssetAddress(string address)
    {
        _assetAddress = address;
    }
    
    public PoolObjectType GetPoolObjectType()
    {
        return _poolObjectType;
    }

    public string GetAssetAddress()
    {
        return _assetAddress;
    }
    
    public void ReturnToPool()
    {
        if(!gameObject.activeSelf)
            return;
        
        PoolManager.Instance.ReturnToPool(_poolObjectType, this);
    }
}
