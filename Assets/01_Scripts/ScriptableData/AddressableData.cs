using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "AddressableData", menuName = "Scriptable Objects/AddressableData")]
public class AddressableData : ScriptableObject
{
    public AssetReference HeroPrefab;
    public AssetReference EnemyPrefab;
    public AssetReference EnemySpawnPrefab;

    [Header("SkillObject")] 
    public AssetReference ChainLightningSkillPrefab;
    public AssetReference OrderSkillPrefab;
}
