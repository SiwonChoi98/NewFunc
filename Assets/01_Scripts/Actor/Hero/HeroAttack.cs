using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    private BaseSkill _baseSkill;
    private void Awake()
    {
        _baseSkill = GetComponentInChildren<BaseSkill>();
    }

    public void SetSkillData(Base_SkillData skillData)
    {
        _baseSkill.SetSkillData(skillData);
    }
    
    public void Attack()
    {
        _baseSkill.SkillAttack();
    }
}
