using System;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    private Base_SkillData _baseSkillData;
    #region Set
    public void SetSkillData(Base_SkillData baseSkillData)
    {
        _baseSkillData = baseSkillData;
        _baseSkillData.SkillBehaviour?.SetData(_baseSkillData.DefaultData, _baseSkillData.BulletData);
    }

    public void SetOwner(Transform owner) => _baseSkillData?.SkillBehaviour.SetOwner(owner);
    #endregion
    
    public void SkillAttack()
    {
        _baseSkillData?.SkillBehaviour.SetTarget(FindTarget());
        _baseSkillData?.SkillBehaviour.Execute();
    }
    
    #region FindTarget

    /// <summary>
    /// _baseSkillData.TargetType에 따라서 목표물으로 갈 수 있거나 해당 방향으로 갈 수 있게 설정  
    /// </summary>
    /// <returns></returns>
    public Transform FindTarget()
    {
        switch (_baseSkillData.targetFindType)
        {
            case SkillTargetFindType.NONE:
                return null;
            case SkillTargetFindType.CLOSEST:
                return FindClosest();
        }

        return null;
    }

    private Transform FindClosest()
    {
        float searchRadius = _baseSkillData.DefaultData.Base_SkillDistance;
        LayerMask targetLayer = LayerMask.GetMask("Enemy");
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, targetLayer);
        if (colliders.Length == 0)
        {
            return null;
        }

        // 가장 가까운 타겟을 찾기 위한 변수
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (var collider in colliders)
        {
            // 각 타겟과의 거리 계산
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            // 가장 가까운 타겟을 선택
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = collider.transform;
            }
        }

        return closestTarget;
    }

    #endregion
    
}
