using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Hero _heroPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    
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
            await UniTask.Delay(3000);
        }
    }
    private async UniTask SpawnEnemyAsync()
    {
        EnemySpawn enemySpawn = ResourceManager.Instance.GetInGameResourceData().EnemySpawn;
        BasePoolObject enemySpawnObject = PoolManager.Instance.SpawnGameObject(PoolObjectType.ENEMY_SPAWN_GO, enemySpawn, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
    
        await UniTask.Delay(1000);
        
        enemySpawnObject.ReturnToPool();
        
        BasePoolObject basePoolObject = PoolManager.Instance.SpawnGameObject(PoolObjectType.ENEMY_GO, _enemyPrefab, enemySpawnObject.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Enemy enemy = basePoolObject as Enemy;
        if (enemy != null)
        {
            BattleManager.Instance.AddManagedEnemy(enemy);
        }
    }
    
}
