using UnityEngine;
using UnityEngine.AddressableAssets;

public class Base_SkillData : ScriptableObject
{
    public SkillType SkillType;
    
    public float Base_SkillDamage;
    public float Base_SkillCooltime;
    public float Base_SkillDistance;
    public float Base_SkillHitCount;
    public float Base_SkillSpawnCount;

    public SkillBehaviour SkillBehaviour;
}
