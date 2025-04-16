using System;
using UnityEngine;

public class HeroAnimationController : MonoBehaviour
{
    private readonly int _doAttack = Animator.StringToHash("DoAttack");
    private readonly int _isMove = Animator.StringToHash("IsMove");

    private Animator _animator;

    private void Awake()
    {
        SetComponent();
    }
    private void SetComponent()
    {
        _animator = GetComponent<Animator>();
    }

    public void AnimMove(Vector2 moveInput)
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        _animator?.SetBool(_isMove, isMoving);
    }

    public void AnimAttack()
    {
        _animator?.SetTrigger(_doAttack);
    }
}
