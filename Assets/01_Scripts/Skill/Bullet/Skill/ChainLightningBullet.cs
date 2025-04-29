using UnityEngine;

public class ChainLightningBullet : BaseBullet
{
    protected override void HitFunction()
    {
        base.HitFunction();
        
        ReturnToPool();
    }

    protected override void Move()
    {
        //Test Target으로 이동
        if (_rigidbody2D == null)
            return;
        
        if(_targetT == null)
            ReturnToPool();
        
        _rigidbody2D.MovePosition(
            Vector2.MoveTowards(_rigidbody2D.position, _targetT.position, _bulletInfo.BulletSpeed * Time.fixedDeltaTime));
    }
}
