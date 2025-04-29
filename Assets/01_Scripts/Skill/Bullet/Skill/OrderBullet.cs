using Cysharp.Threading.Tasks;
using UnityEngine;

public class OrderBullet : BaseBullet
{
    private bool _isBezierMoving = false;
    private bool _isApproachingTarget = true;
    
    protected override void HitFunction()
    {
        base.HitFunction();
        
        _bulletInfo.BulletHitCount--;

        if (_bulletInfo.BulletHitCount <= 0)
        {
            ReturnToPool(); // 마지막 히트 후 종료
        }
    }

    protected override void Move()
    {
        
    }
    private float rotateSpeed = 8f;  // 회전 속도 조절

    private Vector3 moveDir;  // 현재 방향 벡터

    void Start()
    {
        // 초기 방향을 목표와 총알 위치로 설정
    }

    private void FixedUpdate()
    {
        float dirAngle = transform.rotation.eulerAngles.z;
        dirAngle = dirAngle < 0 ? 180 + (180 + dirAngle) : dirAngle;
        if (_targetT) {
            Vector2 dir = transform.right;
            Vector2 targetDir = _targetT.position - transform.position;
            // 바라보는 방향과 타겟 방향 외적
            Vector3 crossVec = Vector3.Cross(dir, targetDir);
            // 상향 벡터와 외적으로 생성한 벡터 내적
            float inner = Vector3.Dot(Vector3.forward, crossVec);
            // 내적이 0보다 크면 오른쪽 0보다 작으면 왼쪽으로 회전
            float addAngle = inner > 0 ? rotateSpeed * Time.fixedDeltaTime : -rotateSpeed * Time.fixedDeltaTime;
            float saveAngle = addAngle + transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, saveAngle);

            float moveDirAngle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
            moveDir = new Vector2(Mathf.Cos(moveDirAngle), Mathf.Sin(moveDirAngle));
            
        }
        Vector2 movePosition = _rigidbody2D.position + (Vector2)moveDir * (_bulletInfo.BulletSpeed * Time.fixedDeltaTime);
        _rigidbody2D.rotation = dirAngle;
        _rigidbody2D.MovePosition(movePosition);
    }
}
