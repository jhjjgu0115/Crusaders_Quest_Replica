public enum Emotion
{
    Normal=0,
    Laugh = 1,
    Sleepy = 2,
    Shout = 3,
    Angry =4,
    Groggy=5,
}

public enum EStatType
{
    None=0,
    //체력
    MaxHealth,
    MinHealth,
    CurruntHealth,

    //공격력
    AttackPoint,

    //치명타
    CriticalRate,
    CirticalMultiplier,

    //SP
    MaxSpecialPoint,
    MinSpecialPoint,
    SpecialPoint,

    //방어관련
    PhysicalDefense,
    MagicalDefense,
    //피해감소
    DamageReduceRate,
    //흡혈율
    BloodSuckingRate,

    //명중률
    MaxAccuracy,
    MinAccuracy,
    Accuracy,
    //회피율
    MaxEvasionRate,
    MinEvasionRate,
    CurrentEvasionRate,
    
    //사거리
    Range,
    //평타속도
    AttackSpeed,
    //관통력
    PhysicalPenetration,
    MagicalPenetration,
    //이동속도
    MoveSpeed,
    //넉백 저항
    KnockbackResistance,
    //시간 가속율
    TimeAccelerationRate,
    //모션 가속율
    MotionAccelerationRate,
    //크기 배율
    ScaleMultiplier
}