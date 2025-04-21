using UnityEngine;
using UnityEngine.UI;
public class ActorView : MonoBehaviour
{
    //기본 체력 UI 등
    [SerializeField] private Image _actorCurrentHealthImage;
    public void UpdateActorView(float health, float maxHealth)
    {
        _actorCurrentHealthImage.fillAmount = health / maxHealth;
    }

}
