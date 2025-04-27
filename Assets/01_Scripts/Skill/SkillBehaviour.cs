using UnityEngine;

/// <summary>
/// 동적 데이터
/// </summary>
public class SkillBehaviour : ScriptableObject
{
    protected Transform _owner;
    protected Transform _target;
    
    public virtual void Execute()
    {
    }

    public virtual void SpawnObject(Vector3 position, Quaternion rotation)
    {
        
    }

    #region Set

    public void SetOwner(Transform owner) => _owner = owner;
    public void SetTarget(Transform target) => _target = target;

    #endregion

}
