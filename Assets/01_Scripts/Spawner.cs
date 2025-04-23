using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _enemySpawnPosList;
    private async void Start()
    {
        await UniTask.WaitUntil(() => AddressableManager.Instance.GetInGameResourceData() != null);
        
        SpwanHero();
        
        //SpawnEnemy();
    }

    private void Update()
    {
        SpawnEnemy_Input();
    }

    #region Spawn

    private async void SpwanHero()
    {
        AssetReference asset = AddressableManager.Instance.GetInGameResourceData().HeroPrefab;
        BasePoolObject assetHero = await PoolManager.Instance.SpawnGameObject(PoolObjectType.HERO_GO, asset,  Vector3.zero, Quaternion.identity);
        if (assetHero == null)
        {
            Debug.LogError("Failed Load Hero");
            return;
        }
        Hero hero = assetHero.GetComponent<Hero>();
        if (hero != null)
        {
            BattleManager.Instance.SetManagedHero(hero);
            hero.GetComponent<ActorState>().SetHealth(100);
        }
    }

    /*private async void SpawnEnemy()
    {
        while (true) // 무한 반복
        {
            await SpawnEnemyAsync();
            await UniTask.Delay(1000);
        }
    }*/
    
    private void SpawnEnemy_Input()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _ = SpawnEnemyAsync();
        }
    }

    #endregion
    
    
    private async UniTask SpawnEnemyAsync()
    {
        Vector3 randomPos = SetEnemyRandomPos();
        AssetReference assetEnemySpawn = AddressableManager.Instance.GetInGameResourceData().EnemySpawnPrefab;
        BasePoolObject enemySpawn = await PoolManager.Instance.SpawnGameObject(PoolObjectType.ENEMY_SPAWN_GO, assetEnemySpawn, randomPos, Quaternion.identity);
        if (enemySpawn == null)
        {
            Debug.LogError("Failed Load EnemySpawn");
            return;
        }
        
        Vector3 enemySpawnPos = enemySpawn.transform.position;
        
        await UniTask.Delay(1000);
        
        enemySpawn.ReturnToPool();
        
        AssetReference assetEnemy = AddressableManager.Instance.GetInGameResourceData().EnemyPrefab;
        BasePoolObject baseEnemyObject = await PoolManager.Instance.SpawnGameObject(PoolObjectType.ENEMY_GO, assetEnemy, enemySpawnPos, Quaternion.identity);
        if (assetEnemy == null)
        {
            Debug.LogError("Failed Load Enemy");
            return;
        }
        
        Enemy enemy = baseEnemyObject as Enemy;
        if (enemy != null)
        {
            BattleManager.Instance.AddManagedEnemy(enemy);
            
            enemy.Initalize(BattleManager.Instance.GetPlayerZoneT());
            enemy.GetComponent<ActorState>().SetHealth(150);
        }
    }
    
    private Vector3 SetEnemyRandomPos()
    {
        int randomIndex = Random.Range(0, _enemySpawnPosList.Count);
        Vector3 randomPosition = new Vector3(
            _enemySpawnPosList[randomIndex].position.x + Random.Range(-0.2f,0.2f), 
            _enemySpawnPosList[randomIndex].position.y + Random.Range(-0.2f,0.2f),
            0);

        return randomPosition;
    }
}
