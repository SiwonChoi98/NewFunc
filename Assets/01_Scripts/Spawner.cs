using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _enemySpawnPosList;
    private void Start()
    {
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
        //scriptableObject Data에서 받아오는 형태로 변경 예정
        string assetAddress = "Hero";
        var assetHero = await AddressableManager.Instance.LoadAssetAsync<GameObject>(assetAddress);
        if (assetHero == null)
        {
            Debug.LogError("Failed Load Hero");
            return;
        }
        
        Hero heroPrefab = assetHero.GetComponent<Hero>();
        
        BasePoolObject basePoolObject = PoolManager.Instance.SpawnGameObject(PoolObjectType.HERO_GO, heroPrefab, Vector3.zero, Quaternion.identity);
        
        //주소 셋팅
        basePoolObject.SetAssetAddress(assetAddress); 
        
        Hero hero = basePoolObject as Hero;
        if (hero != null)
        {
            hero.GetComponent<ActorState>().SetHealth(100);
            BattleManager.Instance.SetManagedHero(hero);
        }
    }

    private async void SpawnEnemy()
    {
        while (true) // 무한 반복
        {
            await SpawnEnemyAsync();
            await UniTask.Delay(1000);
        }
    }
    
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
        string assetEnemySpawnAddress = "EnemySpawn";
        string assetEnemyAddress = "Enemy";
        
        var assetEnemySpawn = await AddressableManager.Instance.LoadAssetAsync<GameObject>(assetEnemySpawnAddress);
        if (assetEnemySpawn == null)
        {
            Debug.LogError("Failed Load EnemySpawn");
            return;
        }
        EnemySpawn enemySpawn = assetEnemySpawn.GetComponent<EnemySpawn>();
        
        Vector3 randomPos = SetEnemyRandomPos();
        BasePoolObject enemySpawnObject = 
            PoolManager.Instance.SpawnGameObject(PoolObjectType.ENEMY_SPAWN_GO, enemySpawn, randomPos, Quaternion.identity);

        enemySpawnObject.SetAssetAddress(assetEnemySpawnAddress);
        
        await UniTask.Delay(1000);
    
        enemySpawnObject.ReturnToPool();
        
        var assetEnemy = await AddressableManager.Instance.LoadAssetAsync<GameObject>(assetEnemyAddress);
        if (assetEnemy == null)
        {
            Debug.LogError("Failed Load Enemy");
            return;
        }
        
        Enemy enemyObjectEnemy = assetEnemy.GetComponent<Enemy>();
        
        BasePoolObject basePoolObject = 
            PoolManager.Instance.SpawnGameObject(PoolObjectType.ENEMY_GO, enemyObjectEnemy, enemySpawnObject.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
        
        basePoolObject.SetAssetAddress(assetEnemyAddress);
        
        Enemy enemy = basePoolObject as Enemy;
        if (enemy != null)
        {
            enemy.Initalize(BattleManager.Instance.GetPlayerZoneT());
            enemy.GetComponent<ActorState>().SetHealth(150);
            
            BattleManager.Instance.AddManagedEnemy(enemy);
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
