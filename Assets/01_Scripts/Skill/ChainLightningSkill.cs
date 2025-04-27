using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Skill/ChainLightningSkill")]
public class ChainLightningSkill : SkillBehaviour
{
    public override void Execute()
    {
        Debug.Log("ChainLightning Skill Execute");
        SpawnObject(_owner.position, Quaternion.identity);
        
        _target?.GetComponent<ActorState>().TakeDamage(20);
    }
    
    public override void SpawnObject(Vector3 position, Quaternion rotation)
    {
        AssetReference prefab = AddressableManager.Instance.GetInGameResourceData().TrailSkillPrefab;
        _ = PoolManager.Instance.SpawnGameObject(PoolObjectType.SKILL_CHAINLIGHTNING_GO, prefab, position, rotation);
    }
}
