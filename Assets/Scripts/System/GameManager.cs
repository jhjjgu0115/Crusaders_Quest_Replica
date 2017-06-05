using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<Unit> heroesList = new List<Unit>();
    public List<Unit> enemyList = new List<Unit>();
    //웨이브 정보


    public Unit leaderHero;


    void EnterTheBattle()
    {
        foreach (Unit _hero in heroesList)
        {
            SetTestHero(_hero);
            _hero.CanInteraction = true;
            _hero.InBattle = true;
        }
    }


    void SetTestHero(Unit _hero)
    {
        foreach (E_StatType _statType in Enum.GetValues(typeof(E_StatType)))
        {
            _hero.StatManager.Create_Stat(_statType, 0);
        }


        _hero.StatManager.CreateOrGetStat(E_StatType.MaxHealth).ModifiedValue = 9798.5f;
        _hero.StatManager.CreateOrGetStat(E_StatType.MinHealth).ModifiedValue = 0;
        _hero.StatManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue = 9798.5f;

        _hero.StatManager.CreateOrGetStat(E_StatType.AttackPoint).ModifiedValue = 860.9f;

        _hero.StatManager.CreateOrGetStat(E_StatType.CriticalRate).ModifiedValue = 21.7f;
        _hero.StatManager.CreateOrGetStat(E_StatType.CirticalMultiplier).ModifiedValue = 2.05f;

        _hero.StatManager.CreateOrGetStat(E_StatType.MaxSpecialPoint).ModifiedValue = 100;
        _hero.StatManager.CreateOrGetStat(E_StatType.MinSpecialPoint).ModifiedValue = 0;
        _hero.StatManager.CreateOrGetStat(E_StatType.CurrentSpecialPoint).ModifiedValue = 0;

        _hero.StatManager.CreateOrGetStat(E_StatType.PhysicalDefense).ModifiedValue = 564.6f;
        _hero.StatManager.CreateOrGetStat(E_StatType.MagicalDefense).ModifiedValue = 464.6f;

        _hero.StatManager.CreateOrGetStat(E_StatType.DamageReduceRate).ModifiedValue = 0;

        _hero.StatManager.CreateOrGetStat(E_StatType.BloodSuckingRate).ModifiedValue = 0;


        _hero.StatManager.CreateOrGetStat(E_StatType.MaxAccuracy).ModifiedValue = 0.75f;
        _hero.StatManager.CreateOrGetStat(E_StatType.MinAccuracy).ModifiedValue = 0f;
        _hero.StatManager.CreateOrGetStat(E_StatType.CurrentAccuracy).ModifiedValue = 0.15f;

        _hero.StatManager.CreateOrGetStat(E_StatType.MaxEvasionRate).ModifiedValue = 0.75f;
        _hero.StatManager.CreateOrGetStat(E_StatType.MinEvasionRate).ModifiedValue = 0f;
        _hero.StatManager.CreateOrGetStat(E_StatType.CurrentEvasionRate).ModifiedValue = 0.15f;

        _hero.StatManager.CreateOrGetStat(E_StatType.MaxRange).ModifiedValue = 3;
        _hero.StatManager.CreateOrGetStat(E_StatType.MinRange).ModifiedValue = 1;
        _hero.StatManager.CreateOrGetStat(E_StatType.CurrentRange).ModifiedValue = 0;


        _hero.StatManager.CreateOrGetStat(E_StatType.MaxAttackSpeed).ModifiedValue = 2.5f;
        _hero.StatManager.CreateOrGetStat(E_StatType.MinAttackSpeed).ModifiedValue = 0.25f;
        _hero.StatManager.CreateOrGetStat(E_StatType.CurrentAttackSpeed).ModifiedValue = 1.4f;

        _hero.StatManager.CreateOrGetStat(E_StatType.PhysicalPenetration).ModifiedValue = 0;
        _hero.StatManager.CreateOrGetStat(E_StatType.MagicalPenetration).ModifiedValue = 0;

        _hero.StatManager.CreateOrGetStat(E_StatType.MoveSpeed).ModifiedValue = 3;

        _hero.StatManager.CreateOrGetStat(E_StatType.KnockbackResistance).ModifiedValue = 0;

        _hero.StatManager.CreateOrGetStat(E_StatType.TimeAccelerationRate).ModifiedValue = 1;
        _hero.StatManager.CreateOrGetStat(E_StatType.MotionAccelerationRate).ModifiedValue = 1;
        _hero.StatManager.CreateOrGetStat(E_StatType.ScaleMultiplier).ModifiedValue = 1;
        _hero.InBattle = true;
    }

    // Use this for initialization
    void Start()
    {

        //EnterTheBattle();
    }


}
