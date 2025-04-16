using UnityEngine;

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

    private void SpawnEnemy()
    {
        BasePoolObject basePoolObject = PoolManager.Instance.SpawnGameObject(PoolObjectType.ENEMY_GO, _enemyPrefab, new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0), Quaternion.identity);
        
        Enemy enemy = basePoolObject as Enemy;
        if (enemy != null)
        {
            BattleManager.Instance.AddManagedEnemy(enemy);
        }
    }
}
