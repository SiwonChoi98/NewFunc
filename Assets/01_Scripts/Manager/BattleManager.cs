using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    private Hero _managedHero;

    public void SetManagedHero(Hero hero)
    {
        _managedHero = hero;
    }
}
