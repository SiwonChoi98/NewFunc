using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : Singleton<ResourceManager>
{
    private InGameResourceData _inGameResourceData;

    protected override void Awake()
    {
        base.Awake();

        //LoadInGameResourceDataAsync();
    }

    private async void Start()
    {
        await LoadInGameResourceDataAsync();
    }

    // 비동기 리소스를 로딩
    private async UniTask LoadInGameResourceDataAsync()
    {
        var asset = await AddressableManager.Instance.LoadAssetAsync<InGameResourceData>("InGameResourceData");
        if (asset == null)
        {
            Debug.LogError("Failed Load InGameResourceData");
            return;
        }
        _inGameResourceData = asset;
    }
    
    /*private async void LoadInGameResourceDataAsync()
    {
        var inGameResourceDataObject = Addressables.LoadAssetAsync<InGameResourceData>("InGameResourceData");
        await inGameResourceDataObject.Task;

        if (inGameResourceDataObject.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError("Failed to load InGameResourceData");
        }

        _inGameResourceData = inGameResourceDataObject.Result;
    }*/

    public InGameResourceData GetInGameResourceData()
    {
        if (_inGameResourceData == null)
            return null;
        
        return _inGameResourceData;
    }
}
