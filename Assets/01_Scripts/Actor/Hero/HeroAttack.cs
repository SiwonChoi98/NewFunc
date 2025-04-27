using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    private BaseSkill _baseSkill;
    
    private void Awake()
    {
        _baseSkill = GetComponentInChildren<BaseSkill>();
    }

    #region Set
    public void SetSkillData(Base_SkillData skillData) => _baseSkill.SetSkillData(skillData);
    public void SetOwner(Transform owner) => _baseSkill.SetOwner(owner);

    #endregion
    
    public void Attack()
    {
        _baseSkill.SkillAttack();
    }
}
