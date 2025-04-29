using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[System.Serializable]
public struct SkillDefaultData
{
    public float Base_SkillDamage;
    public float Base_SkillCooltime;
    public float Base_SkillDistance;
    public float Base_SkillHitCount;
}

[System.Serializable]
public struct SkillBulletData
{
    public float Base_BulletSpeed;
    public float Base_BulletSize;
    public float Base_BulletLifeTime;
    public float Base_BulletSpawnCount;
}
public class Base_SkillData : ScriptableObject
{
    public SkillBehaviour SkillBehaviour;
    
    public SkillNameType skillNameType;
    public SkillTargetFindType targetFindType;
    //public SkillBehaviourType SkillBehaviourType;
    
    [Header("스탯 데이터")]
    [Space]
    public SkillDefaultData DefaultData;
    public SkillBulletData BulletData;
}
