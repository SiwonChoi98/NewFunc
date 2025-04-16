using UnityEngine;

public class ActorAnimationController : MonoBehaviour
{
    private readonly int _isMove = Animator.StringToHash("IsMove");

    protected Animator _animator;
    
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
}
