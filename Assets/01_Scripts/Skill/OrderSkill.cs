using UnityEngine;

[CreateAssetMenu(menuName = "Skill/OrderSkill")]
public class OrderSkill : SkillBehaviour
{
    public override void Execute()
    {
        Debug.Log("Order Skill");
    }
}
