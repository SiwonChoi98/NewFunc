using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<PoolObjectType, Queue<BasePoolObject>> _poolDictionary = new Dictionary<PoolObjectType, Queue<BasePoolObject>>();
    
    protected override void Awake()
    {
        base.Awake();

        SetPoolSizeData();
    }

    private void Start()
    {
        ApplyPoolSizeSettings(_poolSizeData);
    }
    
    //풀 사이즈 조정-------------------------------------------------
    private const string _poolSizeDataPath =  "PoolSizeData";
    private PoolSizeData _poolSizeData;
    private Dictionary<PoolObjectType, int> _poolMaxSizeDictionary = new Dictionary<PoolObjectType, int>();

    private void SetPoolSizeData()
    {
        _poolSizeData = Addressables.LoadAssetAsync<PoolSizeData>(_poolSizeDataPath).WaitForCompletion(); //동기
    }
    private void SetPoolMaxSize(PoolObjectType type, int maxSize)
    {
        _poolMaxSizeDictionary[type] = maxSize;
    }
    public void ApplyPoolSizeSettings(PoolSizeData sizeData)
    {
        foreach (var setting in sizeData.poolSettings)
        {
            if (_poolMaxSizeDictionary.ContainsKey(setting.poolType))
            {
                //에디터 상에서만 중복 경고 메세지 출력
                #if UNITY_EDITOR 
                    Debug.LogWarning($"중복된 풀 타입: {setting.poolType}. 첫번째 설정만 적용됩니다.");
                #endif
            }
            else
            {
                SetPoolMaxSize(setting.poolType, setting.maxSize);
            }
        }
    }
    //-----------------------------------------------------------

    // 생성 -----------------------------------------------------------------------

    public async UniTask<BasePoolObject> SpawnGameObject(PoolObjectType poolObjectType, AssetReference asset, Vector3 pos, Quaternion rotation)
    {
        BasePoolObject poolObject = await SpawnFromPool(poolObjectType, asset, pos, rotation);
    
        if (poolObject == null)
        {
            Debug.LogError($"Failed to spawn object of type {poolObjectType}");
            return null;
        }

        poolObject.SetPoolObjectType(poolObjectType);
        return poolObject;
    }

    /*public BasePoolObject SpawnUI(PoolObjectType poolObjectType, AssetReference asset, Transform transformUI)
    {
        BasePoolObject poolObject = SpawnFromPool(poolObjectType, asset, transformUI);
        poolObject.SetPoolObjectType(poolObjectType);
        return poolObject;
    }*/

    // 해제 -----------------------------------------------------------------------
    public void ReturnToPool(PoolObjectType poolObjectType, BasePoolObject poolObject)
    {
        ReturnToPool(poolObjectType, poolObject, null);
    }

    public void ReturnToPool(PoolObjectType poolObjectType, BasePoolObject poolObject, Transform parent)
    {
        poolObject.transform.SetParent(parent);
        poolObject.gameObject.SetActive(false);

        if (!_poolDictionary.ContainsKey(poolObjectType))
        {
            _poolDictionary[poolObjectType] = new Queue<BasePoolObject>();
        }

        //사이즈 정하지 않은 것은 최대 사이즈
        int maxSize = _poolMaxSizeDictionary.ContainsKey(poolObjectType) ? _poolMaxSizeDictionary[poolObjectType] : int.MaxValue;

        if (_poolDictionary[poolObjectType].Count < maxSize)
        {
            EnqueuePoolObject(poolObjectType, poolObject);
        }
        else
        {
            AddressableManager.Instance.ReleaseAssetInstance(poolObject.gameObject);
        }
    }

    // private 생성/큐 관련 -----------------------------------------------------------------------
    private async UniTask<BasePoolObject> SpawnFromPool(PoolObjectType poolObjectType, AssetReference asset, Vector3 position, Quaternion rotation)
    {
        if (_poolDictionary.TryGetValue(poolObjectType, out Queue<BasePoolObject> queue) && queue.Count > 0)
        {
            BasePoolObject poolObj = DequeuePoolObject(poolObjectType);
            poolObj.gameObject.SetActive(true);
            poolObj.transform.SetPositionAndRotation(position, rotation);
            return poolObj;
        }
        
        GameObject newObj = await CreatePoolObjectAsync(asset, position, rotation);
        if (newObj == null)
        {
            return null;
        }

        return newObj.GetComponent<BasePoolObject>();
    }
    private async UniTask<GameObject> CreatePoolObjectAsync(AssetReference asset, Vector3 pos, Quaternion rotation)
    {
        var handle = asset.InstantiateAsync(pos, rotation);
        await handle.ToUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to instantiate object from asset: {asset.RuntimeKey}");
            return null;
        }

        return handle.Result;
    }
    

    /*private BasePoolObject SpawnFromPool(PoolObjectType poolObjectType, AssetReference asset, Transform spawnTransform)
    {
        if (_poolDictionary.TryGetValue(poolObjectType, out Queue<BasePoolObject> queue) && queue.Count > 0)
        {
            BasePoolObject poolObj = DequeuePoolObject(poolObjectType);
            poolObj.transform.SetParent(spawnTransform);
            poolObj.transform.position = spawnTransform.position;
            poolObj.gameObject.SetActive(true);
            return poolObj;
        }

        AsyncOperationHandle<GameObject> newObject = CreatePoolObject(asset, spawnTransform);
        return newObject.Result.GetComponent<BasePoolObject>();;
    }
    */

    /*private AsyncOperationHandle<GameObject> CreatePoolObject(AssetReference asset, Transform spawnTransform)
    {
        return asset.InstantiateAsync(spawnTransform);
    }*/

    private BasePoolObject DequeuePoolObject(PoolObjectType poolObjectType)
    {
        return _poolDictionary[poolObjectType].Dequeue();
    }

    private void EnqueuePoolObject(PoolObjectType poolObjectType, BasePoolObject poolObject)
    {
        _poolDictionary[poolObjectType].Enqueue(poolObject);
    }
}
