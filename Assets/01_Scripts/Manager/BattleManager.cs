using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : Singleton<BattleManager>
{
    private Hero _managedHero;
    private List<Enemy> _managedEnemies = new List<Enemy>();
    
    [SerializeField] private Transform _playerZoneT;
    
    [Header("PlayerHealth")] 
    [SerializeField] private float _currentHealth;
    
    #region Actor

    public void SetManagedHero(Hero hero)
    {
        _managedHero = hero;
    }

    public void AddManagedEnemy(Enemy enemy)
    {
        _managedEnemies.Add(enemy);
    }

    public void RemoveManagedEnemies()
    {
        Enemy enemy = _managedEnemies[0];
        _managedEnemies.RemoveAt(0);
        enemy.ReturnToPool();
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

    
    //TEST
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }
    
    //TEST
    private void ChangeScene()
    {
        //기존 에셋 메모리 해제
        AddressableManager.Instance.ReleaseAssetAll();
        
        SceneManager.LoadSceneAsync("InGame");
    }
    
}
