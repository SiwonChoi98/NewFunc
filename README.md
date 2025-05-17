# NewFunc

기술 모음집~

## [완료]

### MVP Pattern
  
[https://github.com/SiwonChoi98/NewFunc/tree/main/Assets/01_Scripts/Func/MVPPattern]

구조 (Data <- Present -> View)
  
*ActorState (Model)
- 상태(체력 등)를 보관하고 관리하는 역할
- OnHealthEvent, OnDeathEvent만 통해 외부에 변화 알림 → Presenter만 알 수 있음
- View나 Presenter에 의존하지 않음 → 매우 좋음
      
*Actor (Presenter)
- Model의 이벤트를 구독하여 View를 갱신하거나 로직 처리
- View와 Model 모두 알고 있음
- 데이터 변경 요청은 Model에 위임, 결과 반영은 View에 전달
          
*ActorView (View)
- UI만 담당 (ex: 체력바)
- Presenter(Actor)로부터 받은 정보만 표시
- Model은 몰라도 됨

------------------------------------------------------------------------------------

### Object Pool + Custom Editor (PoolSize)

[https://github.com/SiwonChoi98/NewFunc/tree/main/Assets/01_Scripts/Func/ObjectPool]

게임 오브젝트의 재사용을 통해 성능을 최적화하고, 동적으로 풀 크기를 설정할 수 있는 구조

*BasePoolObject (Model-like 역할)

책임: 풀에 들어갈 오브젝트가 갖춰야 할 기본 정보를 제공하고, 풀로 복귀하는 인터페이스를 제공.

핵심 기능:
- PoolObjectType, AssetAddress 정보 보관
- ReturnToPool() 메서드를 통해 자신을 풀에 반납
의존성: 
- PoolManager에 의존 (자기 자신을 반환할 때 호출)
좋은 점:
- 해당 객체를 재사용에 있어서 사용과 관리가 쉬움

*PoolManager (Singleton, Controller 역할)

책임: 오브젝트 풀의 생성/반환/관리 전반을 담당하는 중앙 관리자

핵심 기능:
- SpawnGameObject() / SpawnUI()로 풀에서 오브젝트 꺼냄
- ReturnToPool()로 풀로 오브젝트 넣기
- 최대 풀 사이즈 제한 기능 -> 부족할 경우 Instantiate()로 새로 생성
- 큐(Dictionary<PoolObjectType, Queue<BasePoolObject>>)로 풀 관리

의존성:
- PoolSizeData(ScriptableObject)
- BasePoolObject
- AddressableManager (에셋에 대한 메모리 해제를 위한 의존)

특징:
- Addressable을 사용한 풀 사이즈 데이터 로드 및 오브젝트 주소 저장
- 큐를 사용하여 가비지 최소화
- 풀 초과 시 Destroy() 처리
- Editor에서 중복 경고 출력 기능까지 포함 → 실수 방지

*PoolSizeData (Data - ScriptableObject)

책임: 
각 PoolObjectType에 대해 최대 풀 사이즈를 지정하는 데이터 컨테이너

구성: 
- 내부 클래스 PoolSizeSetting: 타입 + 최대 사이즈
- 리스트 poolSettings: 각 타입별 설정 리스트

장점:
- 외부에서 데이터 기반으로 풀 크기 조절 가능
- UI나 설정 창에서도 쉽게 수정 가능 (협업의 생산성 증가)

[사용 흐름]
1. [PoolSizeData] 설정 → [PoolManager] 초기화 및 최대 사이즈 등록
2. 풀 사용 요청 → PoolManager.SpawnGameObject / SpawnUI
3. 큐에 여유 있으면 → Dequeue → 오브젝트 반환 큐에 없으면 → Instantiate → 생성
4. 사용 완료 후 → BasePoolObject.ReturnToPool() -> PoolManager.ReturnToPool() → 다시 큐에 저장 or 파괴

------------------------------------------------------------------------------------

### Addressable
  - Manager를 통한 효율적인 관리
  - Object Pool과 연동

 [https://github.com/SiwonChoi98/NewFunc/tree/main/Assets/01_Scripts/Func/Addressable]

 Addressable 과 Object Pool을 통해서 에셋 메모리와 인스턴스 메모리의 최적화

*AddressableManager
 - 에셋 로딩과 해제를 Addressables 방식으로 통합 관리하는 클래스
*PoolManager
- Dictionary<PoolObjectType, Queue<BasePoolObject>>를 사용한 객체 풀링 시스템
- Addressables를 통해 객체를 동적으로 생성하거나 재사용

객체 생성/재사용 (SpawnGameObject):
- 먼저 큐에서 재사용 가능한 객체를 가져오고, 없으면 Addressables로 로드
- 새로운 객체 생성 시 BasePoolObject를 통해 관리

객체 반환 (ReturnToPool):
- 오브젝트를 비활성화하고 큐에 다시 저장
- 최대 크기 초과 시 Addressables 인스턴스를 해제

특징 
- 풀 크기 제한을 두어 메모리 누수를 방지
- 비동기 Addressables 로드와 객체 재사용의 조화
- 에디터 전용 경고로 설정 중복 방지

장점
- 비동기 Addressables + Object Pooling 조합 → 메모리 효율성과 성능 최적화
- 확장 가능하고 유지보수 쉬운 구조
- PoolObjectType 기반으로 유연하게 관리 가능
- ScriptableObject로 설정 관리하여 디자이너 친화적
------------------------------------------------------------------------------------
### Cinemachine Camera Shake Controller

[ https://github.com/SiwonChoi98/NewFunc/blob/main/Assets/01_Scripts/CameraShake.cs ]

------------------------------------------------------------------------------------
### Skill Strategy Pattern

------------------------------------------------------------------------------------

## [진행중]

------------------------------------------------------------------------------------

Resource 사용처
- Hero, Enemy : 직접 그림
- Environment : 에셋 스토어
