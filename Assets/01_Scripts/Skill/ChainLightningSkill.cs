using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Skill/ChainLightningSkill")]
public class ChainLightningSkill : SkillBehaviour
{
    public override void Execute()
    {
        Debug.Log("ChainLightning Skill Execute");
        SpawnObject(_owner.position, Quaternion.identity);
    }
    
    public override async void SpawnObject(Vector3 position, Quaternion rotation)
    {
        AssetReference prefab = AddressableManager.Instance.GetInGameResourceData().ChainLightningSkillPrefab;
        BasePoolObject spawnObject = await PoolManager.Instance.SpawnGameObject(PoolObjectType.SKILL_CHAINLIGHTNING_GO, prefab, position, rotation);
        
        ChainLightningBullet bullet = spawnObject as ChainLightningBullet;
        if (bullet == null)
            return;
        
        spawnObject.GetComponentInChildren<TrailRenderer>().Clear();
        
        int targetLayerNum = Utill.GetTargetLayerTypeToLayerNumber(TargetLayerType.Enemy);
        
        bullet.Initialize(GetBulletInfo(), _target, _owner, targetLayerNum);
    }
}
