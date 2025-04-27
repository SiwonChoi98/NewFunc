using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "ChainLightningData", menuName = "Scriptable Objects/SkillData/ChainLightningData")]
public class ChainLightningData : Base_SkillData
{
    //추가 데이터 들어갈 수 있음
    
    /*public override void SpawnSkillObject(Vector3 position, Quaternion rotation)
    {
        AssetReference prefab = AddressableManager.Instance.GetInGameResourceData().TrailSkillPrefab;
        AddressableManager.Instance.InstantiateAssetInstance(prefab, position, rotation);
    }*/
}
