using System;
using UnityEngine;

public class Hero : Actor
{
    private HeroAttack _heroAttack;
    
    #region UnityLifeSycle
    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        InputMove();
        InputAttack();
        InputSetSkillData();
    }
    
    #endregion

    protected override void SetComponent()
    {
        base.SetComponent();

        _heroAttack = GetComponent<HeroAttack>();
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
        _actorAnimationController.AnimMove(_moveInput);
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
            if (_actorAnimationController is HeroAnimationController)
            {
                HeroAnimationController heroAnimationController = _actorAnimationController as HeroAnimationController;
                heroAnimationController.AnimAttack();

                _heroAttack.Attack();
                //BattleManager.Instance.FirstEnemyDamage(30);
            }
            
        }
    }

    private void InputSetSkillData()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Base_SkillData skillData = ResourceManager.Instance.GetBaseSkillData(SkillType.CHAINLIGHTING);
            _heroAttack.SetSkillData(skillData);
            _heroAttack.SetOwner(this.transform);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Base_SkillData skillData = ResourceManager.Instance.GetBaseSkillData(SkillType.ORDER);
            _heroAttack.SetSkillData(skillData);
            _heroAttack.SetOwner(this.transform);
        }
    }
    
}
