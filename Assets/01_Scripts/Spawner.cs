using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Hero _heroPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    
    [SerializeField] private List<Transform> _enemySpawnPosList;
    private void Start()
    {
        SpwanHero();
        
        SpawnEnemy();
    }

    private void SpwanHero()
    {
        BasePoolObject basePoolObject = PoolManager.Instance.SpawnGameObject(PoolObjectType.HERO_GO, _heroPrefab, Vector3.zero, Quaternion.identity);
        
        Hero hero = basePoolObject as Hero;
        if (hero != null)
        {
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
    private async UniTask SpawnEnemyAsync()
    {
        EnemySpawn enemySpawn = ResourceManager.Instance.GetInGameResourceData().EnemySpawn;

        Vector3 randomPos = SetEnemyRandomPos();
        BasePoolObject enemySpawnObject = PoolManager.Instance.SpawnGameObject(PoolObjectType.ENEMY_SPAWN_GO, enemySpawn, randomPos, Quaternion.identity);
    
        await UniTask.Delay(1000);
        
        enemySpawnObject.ReturnToPool();
        
        BasePoolObject basePoolObject = PoolManager.Instance.SpawnGameObject(PoolObjectType.ENEMY_GO, _enemyPrefab, enemySpawnObject.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
        Enemy enemy = basePoolObject as Enemy;
        if (enemy != null)
        {
            enemy.Initalize(BattleManager.Instance.GetPlayerZoneT());
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
