using UnityEngine;

/// <summary>
/// 동적 데이터
/// </summary>
public class SkillBehaviour : ScriptableObject
{
    protected Transform _owner;
    protected Transform _target;
    
    protected SkillDefaultData _defaultData;
    protected SkillBulletData _bulletData;
    
    public virtual void Execute()
    {
    }

    public virtual void SpawnObject(Vector3 position, Quaternion rotation)
    {
        
    }

    #region Set
    public void SetOwner(Transform owner) => _owner = owner;
    public void SetTarget(Transform target) => _target = target;

    public void SetData(SkillDefaultData skillDefaultData, SkillBulletData skillBulletData)
    {
        _defaultData = skillDefaultData;
        _bulletData = skillBulletData;
    }

    #endregion


    #region Get

    protected BulletInfo GetBulletInfo()
    {
        BulletInfo bulletInfo = new BulletInfo
        {
            BulletDamage = _defaultData.Base_SkillDamage,
            BulletSpeed = _bulletData.Base_BulletSpeed,
            BulletLifeTime = _bulletData.Base_BulletLifeTime,
            BulletSize = _bulletData.Base_BulletSize,
            BulletHitCount = _defaultData.Base_SkillHitCount
        };

        return bulletInfo;
    }

    #endregion

}
