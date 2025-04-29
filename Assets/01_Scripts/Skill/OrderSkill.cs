using Cysharp.Threading.Tasks;
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
    
    public override async void SpawnObject(Vector3 position, Quaternion rotation)
    {
        AssetReference prefab = AddressableManager.Instance.GetInGameResourceData().OrderSkillPrefab;
        BasePoolObject spawnObject = await PoolManager.Instance.SpawnGameObject(PoolObjectType.SKILL_ORDER_GO, prefab, position, rotation);
        
        OrderBullet bullet = spawnObject as OrderBullet;
        if (bullet == null)
            return;
        
        int targetLayerNum = Utill.GetTargetLayerTypeToLayerNumber(TargetLayerType.Enemy);
        
        bullet.Initialize(GetBulletInfo(), _target, _owner, targetLayerNum);
    }
    
}
