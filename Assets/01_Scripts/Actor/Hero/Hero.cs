using System;
using UnityEngine;

public class Hero : Actor
{
    private HeroAnimationController _heroAnimationController;
    private HeroView _heroView;
    private HeroState _heroState;
    
    private Vector2 _moveInput;
    [SerializeField] private float _moveSpeed = 5f;
    
    #region UnityLifeSycle
    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        InputMove();
        InputAttack();
    }
    
    #endregion

    protected override void SetComponent()
    {
        base.SetComponent();
        
        _heroAnimationController = GetComponentInChildren<HeroAnimationController>();
        _heroView = GetComponentInChildren<HeroView>();
        
        //구조 다시 생각해봐야함
        _heroState = GetComponent<HeroState>();
    }

    //입력
    private void InputMove()
    {
        //이동 입력
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _moveInput = new Vector2(h, v).normalized;
        
        //플립 여부
        if (Mathf.Abs(h) > 0.01f)
        {
            _spriteRenderer.flipX = h > 0;
        }

        //이동 애니메이션
        _heroAnimationController.AnimMove(_moveInput);
    }

    //실제 입력 기반 이동
    private void Move()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveInput * (_moveSpeed * Time.fixedDeltaTime));
    }

    //공격 입력
    private void InputAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _heroAnimationController.AnimAttack();
        }
    }
    
}
