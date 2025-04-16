using System;
using UnityEngine;

public class HeroAnimationController : ActorAnimationController
{
    private readonly int _doAttack = Animator.StringToHash("DoAttack");
    
    public void AnimAttack()
    {
        _animator?.SetTrigger(_doAttack);
    }
}
