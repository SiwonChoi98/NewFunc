using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public struct BulletInfo
{
    public float BulletDamage;
    public float BulletSpeed;
    public float BulletLifeTime; 
    public float BulletSize;
    public float BulletHitCount;
}

public class BaseBullet : BasePoolObject
{
    protected Rigidbody2D _rigidbody2D;
    protected Transform _targetT;
    protected Transform _ownerT;
    
    protected int _targetLayerNumber = -1;
    
    protected BulletInfo _bulletInfo;
    public virtual void Initialize(BulletInfo bulletInfo, Transform targetT, Transform ownerT, int targetLayerNumber)
    {
        _bulletInfo = bulletInfo;
        _targetT = targetT;
        _ownerT = ownerT;
        _targetLayerNumber = targetLayerNumber;

        SetBulletSize(_bulletInfo.BulletSize);
    }
    protected virtual void Move(){}
    
    //자체 자전이며 다른 회전 등은 하위 클래스에서 구현
    protected virtual void Rotate(){}

    //충돌 시 기능
    protected virtual void HitFunction(){}

    private void SetBulletSize(float bulletSize)
    {
        transform.localScale = new Vector3(bulletSize, bulletSize, 1);
    }

    #region UnityLifeSycle

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (_targetLayerNumber == other.gameObject.layer)
        {
            other.GetComponent<ActorState>().TakeDamage(_bulletInfo.BulletDamage);
            HitFunction();
        }
    }

    #endregion
    
}
