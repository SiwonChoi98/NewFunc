# NewFunc

기술 모음집~

[완료]

- MVP Pattern
  Data <- Present -> View
  [https://github.com/SiwonChoi98/NewFunc/tree/main/Assets/01_Scripts/Func/MVPPattern]

  구조
  
  ActorState (Model)
    - 상태(체력 등)를 보관하고 관리하는 역할
    - OnHealthEvent, OnDeathEvent만 통해 외부에 변화 알림 → Presenter만 알 수 있음
    - View나 Presenter에 의존하지 않음 → 매우 좋음
      
  Actor (Presenter)
    - Model의 이벤트를 구독하여 View를 갱신하거나 로직 처리
    - View와 Model 모두 알고 있음
    - 데이터 변경 요청은 Model에 위임, 결과 반영은 View에 전달
          
  ActorView (View)
    - UI만 담당 (ex: 체력바)
    - Presenter(Actor)로부터 받은 정보만 표시
    - Model은 몰라도 됨
  
- Object Pool + Custom Editor (PoolSize)
  효율적인 메모리 관리
  [https://github.com/SiwonChoi98/NewFunc/tree/main/Assets/01_Scripts/Func/ObjectPool]
  
  

[진행중]

- Addressable
  - Manager를 통한 효율적인 관리
  - Object Pool과 연동

[계획]

- Red Dot
- Skill Strategy Pattern
- Camera Shake Controller


Resource
- Hero, Enemy : 직접 그림
- Environment : 에셋 스토어
