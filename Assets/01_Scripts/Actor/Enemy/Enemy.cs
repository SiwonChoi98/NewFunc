using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Enemy : Actor
{
    private Transform _targetT;
    public void Initalize(Transform targetT)
    {
        _targetT = targetT;
        
        SetFlip();
    }
    
    #region UnityLifeSycle
    private void FixedUpdate()
    {
        //Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerZone"))
        {
            BattleManager.Instance.TakeDamage(5);
            RemoveActor();
        }
    }

    #endregion
    
    private void SetFlip()
    {
        if (_targetT == null)
            return;

        _spriteRenderer.flipX = _targetT.position.x > transform.position.x ? true : false;
    }
    
    private void Move()
    {
        if (_targetT == null) 
            return;

        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 targetPosition = _targetT.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;

        _rigidbody2D.MovePosition(currentPosition + direction * (_moveSpeed * Time.fixedDeltaTime));
    }
    
    
    #region AnimationIndex 방식

    [SerializeField] private List<Sprite> _animSprites;
    private int _animCurrentFrame = 0;

    private void Start()
    {
        //Animate().Forget();
    }

    private async UniTaskVoid Animate()
    {
        while (true)
        {
            _spriteRenderer.sprite = _animSprites[_animCurrentFrame];
            _animCurrentFrame = (_animCurrentFrame + 1) % _animSprites.Count;
            await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        }
    }
    
    #endregion
}
