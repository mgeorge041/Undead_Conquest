public enum Team
{
    Hero,
    Villain,
    None
};


public enum PlayerStateType
{
    Deploy,
    Action,
    Ability,
    Attack,
    Rescue,
    Neutral,
    Info,
    None,
}

public enum CharacterType
{
    Hero,
    Villain,
    NPC,
    Test,
    None
}

public enum CharacterActionType
{
    Ability,
    Attack,
    Flee,
    Move,
    Rescue,
    None
}

public enum HexListType
{
    Attack,
    Move,
    NPC,
    Rescue,
    Deploy,
    Ability,
    Select,
    Splash,
    AttackRescue,
    AttackMove,
    RescueMove,
    AttackRescueMove,
    None
}

public enum StatusType
{
    Base,
    Floating,
    Dead,
    Default,
    None
}

public enum TargetType
{
    Friendly,
    Enemy,
    None
}

public enum AbilityType
{
    Buff,
    Creation,
    Damage,
    Debuff,
    Movement,
    Projectile,
    Status,
    None
}

public enum TargetPattern
{
    Line,
    Diagonal,
    Range,
    None
}

public enum EffectPattern
{
    Line,
    Ring,
    Wall,
    VShape,
    TShape,
    Fan,
    SpreadV,
    Area,
    Pierce,
    Target,
    None
}

public enum CharacterStatType
{
    Strength,
    AttackRange,
    Defense,
    Speed,
    RemainingSpeed,
    Health,
    CurrentHealth,
    Rescue,
    RescueRange,
    CurrentRescue,
    None
}

public enum TerrainPieceStatType
{
    Health,
    CurrentHealth,
    Duration,
    RemainingDuration,
    None
}

public enum GamePieceType
{
    Character,
    Terrain,
    None
}

public enum ProjectileType
{
    AOE,
    Pierce,
    SingleTarget,
    None
}

public enum ProjectilePattern
{
    AOE,
    Line,
    Pierce,
    TShape,
    VShape,
    None
}

public enum PlayerType
{
    Human,
    Computer,
    None
} 

public enum PersonalityType
{
    Aggressive,
    Defensive,
    Helping,
    Balanced,
    Random,
    Chaotic,
    None
}

public enum CharacterOutlineType
{
    Inset,
    Outline,
    None
}