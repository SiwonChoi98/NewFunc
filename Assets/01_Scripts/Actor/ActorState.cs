using System;
using UnityEngine;

public class ActorState : MonoBehaviour
{
    private float _actorCurrentHealth;
    private float _actorMaxHealth;
    public Action<float, float> OnHealthEvent; 
    public Action OnDeathEvent;
    public void SetHealth(float health)
    {
        _actorMaxHealth = health;
        _actorCurrentHealth = _actorMaxHealth;
        OnHealthEvent?.Invoke(_actorCurrentHealth, _actorMaxHealth);
    }

    public void AddHealth(float health)
    {
        _actorCurrentHealth += health;
        OnHealthEvent?.Invoke(_actorCurrentHealth, _actorMaxHealth);
    }

    public void TakeDamage(float health)
    {
        _actorCurrentHealth -= health;
        OnHealthEvent?.Invoke(_actorCurrentHealth, _actorMaxHealth);

        if (_actorCurrentHealth <= 0)
        {
            OnDeathEvent?.Invoke();    
        }
    }
}
