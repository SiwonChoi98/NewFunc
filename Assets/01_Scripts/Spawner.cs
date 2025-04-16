using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Hero _heroPrefab;
    private void Start()
    {
        SpwanHero();
    }

    private void SpwanHero()
    {
        BasePoolObject basePoolObject = PoolManager.Instance.SpawnGameObject(PoolObjectType.HERO_GO, _heroPrefab, Vector3.zero, Quaternion.identity);
        if (basePoolObject is Hero)
        {
            Hero hero = basePoolObject as Hero;
            BattleManager.Instance.SetManagedHero(hero);
        }
    }
}
