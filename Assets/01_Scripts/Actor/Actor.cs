using UnityEngine;

public class Actor : BasePoolObject
{
    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;
    
    
    protected ActorAnimationController _actorAnimationController;
    protected ActorView _actorView;
    
    protected Vector2 _moveInput;
    [SerializeField] protected float _moveSpeed = 5f;
    
    #region UnityLifeSycle
    private void Awake()
    {
        SetComponent();
    }
    
    #endregion
    
    
    #region Set

    protected virtual void SetComponent()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        _actorAnimationController = GetComponentInChildren<ActorAnimationController>();
        _actorView = GetComponentInChildren<ActorView>();
    }

    #endregion
}
