using UnityEngine;

public enum PoolObjectType
{
    HERO_GO = 1001,
    ENEMY_GO = 1002,
    
    DAMAGETEXT_GO = 1003,
    ENEMY_SPAWN_GO = 1005,
    
    
    SKILL_CHAINLIGHTNING_GO = 5006,
    SKILL_ORDER_GO = 5007,
    
}

public enum SkillType
{
    CHAINLIGHTING = 1,
    ORDER = 2,
}

public enum SkillTargetType
{
    NONE = 1,       // 목표 없음
    CLOSEST = 2,    // 가장 가까운 적
}