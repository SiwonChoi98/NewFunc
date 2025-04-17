using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    //Addressable 교체 예정
    private const string _inGameResourceDataPath = "InGameResourceData";
    private InGameResourceData _inGameResourceData;

    protected override void Awake()
    {
        base.Awake();

        SetInGameResourceData();
    }

    private void SetInGameResourceData()
    {
        _inGameResourceData = Resources.Load<InGameResourceData>(_inGameResourceDataPath);
    }

    public InGameResourceData GetInGameResourceData()
    {
        if (_inGameResourceData == null)
            return null;
        
        return _inGameResourceData;
    }
}
