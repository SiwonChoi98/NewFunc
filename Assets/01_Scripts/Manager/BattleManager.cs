using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : Singleton<BattleManager>
{
    private Hero _managedHero;
    private List<Enemy> _managedEnemies = new List<Enemy>();
    
    [SerializeField] private Transform _playerZoneT;
    [SerializeField] private CinemachineBrain _mainCamera;
    
    [Header("PlayerHealth")] 
    [SerializeField] private float _currentHealth;
    
    #region Actor

    public void SetManagedHero(Hero hero)
    {
        _managedHero = hero;
    }

    public void RemoveManagedHero()
    {
        _managedHero = null;
    }
    
    
    public void AddManagedEnemy(Enemy enemy)
    {
        _managedEnemies.Add(enemy);
    }

    public void RemoveManagedEnemy(Actor enemyActor)
    {
        Enemy enemy =  enemyActor as Enemy;
        _managedEnemies.Remove(enemy);
    }
    

    #endregion
    #region Get

    public Transform GetPlayerZoneT()
    {
        return _playerZoneT;
    }

    #endregion
    #region Set
    

    #endregion

    #region Func

    private void CameraShake(float intensity, float duration)
    {
        //현재 활성화된 VirtualCamera 가져오기 (ICinemachineCamera 타입)
        ICinemachineCamera activeCam = _mainCamera.ActiveVirtualCamera;
        //VirtualCameraGameObject를 통해 GameObject 접근
        CinemachineCamera camObj = (activeCam as CinemachineVirtualCameraBase)?.GetComponent<CinemachineCamera>();
        
        CameraShake cameraShake = camObj?.GetComponent<CameraShake>();
        cameraShake?.Shake(intensity, duration); // 원하는 세기로 흔들기
    }

    #endregion

    #region TEST
    
    public void TakeDamage(float damage)
    {
        CameraShake(1, 0.5f);
        _currentHealth -= damage;
    }
    
    private void ChangeScene()
    {
        //기존 에셋 메모리 해제
        //AddressableManager.Instance.ReleaseAssetAll();
        
        SceneManager.LoadSceneAsync("InGame");
    }
    public void FirstEnemyDamage(float damage)
    {
        _managedEnemies[0].ActorState.TakeDamage(damage);
    }

    #endregion
    
}
