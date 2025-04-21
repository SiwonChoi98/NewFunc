using System;
using UnityEngine;

public class Actor : BasePoolObject
{
    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;
    
    
    protected ActorAnimationController _actorAnimationController;
    protected ActorView _actorView;
    public ActorView ActorView => _actorView;
    
    protected ActorState _actorState;
    public ActorState ActorState => _actorState;
    
    protected Vector2 _moveInput;
    [SerializeField] protected float _moveSpeed = 5f;
    
    #region UnityLifeSycle
    private void Awake()
    {
        SetComponent();
        SubscribeActionEvent();
    }

    private void OnDestroy()
    {
        UnSubscribeActionEvent();
    }

    #endregion
    
    
    #region Set

    protected virtual void SetComponent()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        _actorAnimationController = GetComponentInChildren<ActorAnimationController>();
        _actorView = GetComponentInChildren<ActorView>();
        _actorState = GetComponent<ActorState>();
    }
    #endregion
    
    /// <summary>
    /// MVP Pattern
    
    ///ActorState (Model)
    ///상태(체력 등)를 보관하고 관리하는 역할
    ///OnHealthEvent, OnDeathEvent만 통해 외부에 변화 알림 → Presenter만 알 수 있음
    ///View나 Presenter에 의존하지 않음
    
    ///Actor (Presenter)
    ///Model의 이벤트를 구독하여 View를 갱신하거나 로직 처리
    ///View와 Model 모두 알고 있음
    ///데이터 변경 요청은 Model에 위임, 결과 반영은 View에 전달
    
    /// ActorView (View)
    /// UI만 담당 (ex: 체력바)
    /// Presenter(Actor)로부터 받은 정보만 표시
    
    /// </summary>
    private void SubscribeActionEvent()
    {
        _actorState.OnHealthEvent += _actorView.UpdateActorView;
        _actorState.OnDeathEvent += RemoveActor;
    }

    private void UnSubscribeActionEvent()
    {
        _actorState.OnHealthEvent -= _actorView.UpdateActorView;
        _actorState.OnDeathEvent -= RemoveActor;
    }
    
    //EnemyView - HeroView 구분 시 오버라이드로 정의
    public void RemoveActor()
    {
        switch (GetPoolObjectType())
        {
            case PoolObjectType.HERO_GO:
                BattleManager.Instance.RemoveManagedHero();
                break;
            case PoolObjectType.ENEMY_GO:
                BattleManager.Instance.RemoveManagedEnemy(this);
                break;
        }
        
        ReturnToPool();
    }

    
}
