using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    private Hero _managedHero;
    private List<Enemy> _managedEnemies = new List<Enemy>();
        
    public void SetManagedHero(Hero hero)
    {
        _managedHero = hero;
    }

    public void AddManagedEnemy(Enemy enemy)
    {
        _managedEnemies.Add(enemy);
    }
}
