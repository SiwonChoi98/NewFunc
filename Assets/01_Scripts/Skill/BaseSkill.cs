using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    private Base_SkillData _baseSkillData;

    public void SetSkillData(Base_SkillData baseSkillData)
    {
        _baseSkillData = baseSkillData;
    }

    public void SkillAttack()
    {
        _baseSkillData?.SkillBehaviour.Execute();
    }
    
}
