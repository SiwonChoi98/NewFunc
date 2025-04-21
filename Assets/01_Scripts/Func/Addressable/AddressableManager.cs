using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : Singleton<AddressableManager>
{
    
    private Dictionary<string, AsyncOperationHandle> _loadedAssets = new Dictionary<string, AsyncOperationHandle>();
    
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
        // 이미 로드된 자산이 있으면 바로 반환
        if (_loadedAssets.TryGetValue(address, out var existingHandle))
        {
            if (existingHandle.Status == AsyncOperationStatus.Succeeded)
            {
                return existingHandle.Result as T;
            }
            else
            {
                Debug.LogWarning($"[AddressableManager] Asset at address '{address}' is not loaded properly.");
            }
        }
        
        AsyncOperationHandle<T> handle;

        try
        {
            handle = Addressables.LoadAssetAsync<T>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadedAssets[address] = handle;  //loadAsset 추가
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
        var handle = Addressables.LoadAssetAsync<T>(address);
        var result = handle.WaitForCompletion();

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return result;
        }
        else
        {
            Debug.LogError($"Failed to load asset at address: {address}");
            return null;
        }
    }
    public void ReleaseAsset(string address)
    {
        if (_loadedAssets.TryGetValue(address, out AsyncOperationHandle handle))
        {
            Addressables.Release(handle);
            _loadedAssets.Remove(address);
        }
        else
        {
            Debug.LogWarning($"[AddressableManager] Tried to release asset not tracked: {address}");
        }
    }
    
    //전부 해제
    public void ReleaseAssetAll()
    {
        foreach (AsyncOperationHandle handleObj in _loadedAssets.Values)
        {
            Addressables.Release(handleObj);
        }

        _loadedAssets.Clear();
    } 
    
    
    //Addressable 생성
    public AsyncOperationHandle<GameObject> InstantiateAssetInstance(string key)
    {
        return Addressables.InstantiateAsync(key);
    }
    
    //Addressable 해제
    public void ReleaseAssetInstance(GameObject addressableObject)
    {
        Addressables.ReleaseInstance(addressableObject);
    }
    
}
