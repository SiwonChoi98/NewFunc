using UnityEngine;

public class Actor : BasePoolObject
{
    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;
    
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
    }

    #endregion
}
