using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

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

    public BasePoolObject SpawnGameObject(PoolObjectType poolObjectType, BasePoolObject basePoolObject, Vector3 pos, Quaternion rotaion)
    {
        BasePoolObject poolObject = SpawnFromPool(poolObjectType, basePoolObject, pos, rotaion);
        poolObject.SetPoolObjectType(poolObjectType);
        return poolObject;
    }

    public BasePoolObject SpawnUI(PoolObjectType poolObjectType, BasePoolObject basePoolObject, Transform transformUI)
    {
        BasePoolObject poolObject = SpawnFromPool(poolObjectType, basePoolObject, transformUI);
        poolObject.SetPoolObjectType(poolObjectType);
        return poolObject;
    }

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
            //최대 크기 넘어가면 삭제
            Destroy(poolObject.gameObject);
            
            //addressable 메모리 해제
            if (_poolDictionary[poolObjectType].Count == 0)
            {
                string address = poolObject.GetAssetAddress();
                AddressableManager.Instance.ReleaseAsset(address);
            }
        }
    }

    // private 생성/큐 관련 -----------------------------------------------------------------------
    private BasePoolObject SpawnFromPool(PoolObjectType poolObjectType, BasePoolObject poolObject, Vector3 position, Quaternion rotation)
    {
        if (_poolDictionary.TryGetValue(poolObjectType, out Queue<BasePoolObject> queue) && queue.Count > 0)
        {
            BasePoolObject poolObj = DequeuePoolObject(poolObjectType);
            poolObj.gameObject.SetActive(true);
            poolObj.transform.SetPositionAndRotation(position, rotation);
            return poolObj;
        }
        return CreatePoolObject(poolObject, position, rotation);
    }

    private BasePoolObject SpawnFromPool(PoolObjectType poolObjectType, BasePoolObject poolObject, Transform spawnTransform)
    {
        if (_poolDictionary.TryGetValue(poolObjectType, out Queue<BasePoolObject> queue) && queue.Count > 0)
        {
            BasePoolObject poolObj = DequeuePoolObject(poolObjectType);
            poolObj.transform.SetParent(spawnTransform);
            poolObj.transform.position = spawnTransform.position;
            poolObj.gameObject.SetActive(true);
            return poolObj;
        }
        return CreatePoolObject(poolObject, spawnTransform);
    }

    private BasePoolObject CreatePoolObject(BasePoolObject poolObject, Vector3 pos, Quaternion rotation)
    {
        return Instantiate(poolObject, pos, rotation);
    }

    private BasePoolObject CreatePoolObject(BasePoolObject poolObject, Transform spawnTransform)
    {
        return Instantiate(poolObject, spawnTransform);
    }

    private BasePoolObject DequeuePoolObject(PoolObjectType poolObjectType)
    {
        return _poolDictionary[poolObjectType].Dequeue();
    }

    private void EnqueuePoolObject(PoolObjectType poolObjectType, BasePoolObject poolObject)
    {
        _poolDictionary[poolObjectType].Enqueue(poolObject);
    }
}
