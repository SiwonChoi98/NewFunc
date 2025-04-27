using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "OrderData", menuName = "Scriptable Objects/SkillData/OrderData")]
public class OrderData : Base_SkillData
{
    //추가 데이터 들어갈 수 있음
    
    /*public override void SpawnSkillObject(Vector3 position, Quaternion rotation)
    {
        AssetReference prefab = AddressableManager.Instance.GetInGameResourceData().DefaultSkillPrefab;
        AddressableManager.Instance.InstantiateAssetInstance(prefab, position, rotation);
    }*/
}
