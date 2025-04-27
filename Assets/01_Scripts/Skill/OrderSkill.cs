using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Skill/OrderSkill")]
public class OrderSkill : SkillBehaviour
{
    public override void Execute()
    {
        Debug.Log("Order Skill Execute");
        SpawnObject(_owner.position, Quaternion.identity);
    }
    
    public override void SpawnObject(Vector3 position, Quaternion rotation)
    {
        AssetReference prefab = AddressableManager.Instance.GetInGameResourceData().DefaultSkillPrefab;
        _ = PoolManager.Instance.SpawnGameObject(PoolObjectType.SKILL_ORDER_GO, prefab, position, rotation);
    }
}
