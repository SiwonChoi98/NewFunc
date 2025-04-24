using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : Singleton<AddressableManager>
{
    private AddressableData _addressableData;
    public AddressableData GetInGameResourceData()
    {
        if (_addressableData == null)
            return null;
        
        return _addressableData;
    }
    protected override void Awake()
    {
        base.Awake();
        
        LoadAddressableData("AddressableData");
    }
    
    //Load Data
    private async void LoadAddressableData(string address)
    {
        try
        {
            AddressableData asset = await Addressables.LoadAssetAsync<AddressableData>(address);
            _addressableData = asset;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed Load LoadData" + e.Message);
        }
    }
    
    
    //Addressable 생성
    public AsyncOperationHandle<GameObject> InstantiateAssetInstance(AssetReference prefab, Vector3 pos, Quaternion rot)
    {
        return prefab.InstantiateAsync(pos, rot);
        /*myPrefab.InstantiateAsync().Completed += handle =>
        {
            GameObject obj = handle.Result;
            // 생성된 오브젝트 사용 가능
        };*/
    }
    
    //Addressable 해제
    /// <summary>
    /// 해당 메서드는 clone에 대한 오브젝트 메모리를 내리게 되고, 참조 카운트가 0이 되면 내부적으로 에셋에 대한 메모리도 내리는 형태
    /// </summary>
    /// <param name="addressableObject"></param>
    public void ReleaseAssetInstance(GameObject addressableObject)
    {
        Addressables.ReleaseInstance(addressableObject);
        
        //Test용 즉시 해제
        //Resources.UnloadUnusedAssets();
    }

    public void ReleaseAsset(AsyncOperationHandle<GameObject> addressableObject)
    {
        Addressables.Release(addressableObject);
    }
}
