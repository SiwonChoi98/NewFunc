using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

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

    
    //비동기 로드
    /// <summary>
    /// 1. Scriptable은 바로 해당 클래스로 접근 가능하다
    /// 2. GameObject는 GameObject 이후 GetComponent로 접근해야 함
    /// </summary>
    /// <param name="address"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async UniTask<T> LoadAssetAsync<T>(string address) where T : UnityEngine.Object
    {
        AsyncOperationHandle<T> handle;

        try
        {
            handle = Addressables.LoadAssetAsync<T>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            else
            {
                Debug.LogError($"[AddressableManager] Failed to load asset at address: {address}, Status: {handle.Status}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[AddressableManager] Exception occurred while loading asset at address: {address}\n{e}");
        }

        return default;
    }
    
    /*public async UniTask<GameObject> SpawnLoadAssetGameObject(string address)
    {
        GameObject addressableGameObject = await LoadAssetAsync<GameObject>(address);
        if (addressableGameObject == null)
            return null;
        
        return addressableGameObject;
    }*/
    
    //동기 로드
    public T LoadAssetSync<T>(string address) where T : UnityEngine.Object
    {
        T handle = Addressables.LoadAssetAsync<T>(address).WaitForCompletion();
        
        if(handle == null)
        {
            Debug.LogError($"Failed to load asset at address: {address}");
            return null;
        }

        return handle;
    }
    
    //Addressable 생성 -> PoolManager에서 책임
    /*public AsyncOperationHandle<GameObject> InstantiateAssetInstance(AssetReference prefab)
    {
        return prefab.InstantiateAsync();
    }
    public AsyncOperationHandle<GameObject> InstantiateAssetInstance(AssetReference prefab, Vector3 pos, Quaternion rot)
    {
        return prefab.InstantiateAsync(pos, rot);
    }*/
    
    
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
    
}
