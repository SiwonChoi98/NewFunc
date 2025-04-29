using System;
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
}
