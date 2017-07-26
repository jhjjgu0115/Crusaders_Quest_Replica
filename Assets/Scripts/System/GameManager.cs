using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance)
            {
                return instance;
            }
            else
            {
                instance = FindObjectOfType<GameManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "GameManager";
                    instance = container.AddComponent<GameManager>();
                }
                return instance;
            }
        }
    }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    static List<Unit> playerUnitList = new List<Unit>();

    public static List<Unit> PlayerUnitList
    {
        get
        {
            return playerUnitList;
        }
    }
    public List<Unit> enemyUnitList = new List<Unit>();
    public static Unit playerHeadUnit;
    public static Unit monsterHeadUnit;


    //웨이브 정보


    Unit targetUnit;
    int targetUnitNum=0;
    public Unit leaderHero;


    public void UseChainBlock(int skillNum)
    {
        targetUnit.skillQueue.AddAction(targetUnit.skillList[skillNum]);
    }
    public void ChangeSkillTarget()
    {
        //2명일때
        targetUnitNum++;
        if(targetUnitNum>=playerUnitList.Count)
        {
            targetUnitNum = 0;
        }
        targetUnit = playerUnitList[targetUnitNum];
    }





    void EnterTheBattle()
    {
        foreach (Unit _hero in playerUnitList)
        {
            SetTestHero(_hero);
        }
        foreach (Unit _hero in enemyUnitList)
        {
            SetTestHero(_hero);
        }
    }


    void SetTestHero(Unit _hero)
    {
        foreach (E_StatType _statType in Enum.GetValues(typeof(E_StatType)))
        {
            _hero.StatManager.CreateOrGetStat(_statType);
        }


        _hero.StatManager.CreateOrGetStat(E_StatType.MaxHealth).ModifiedValue = 9798.5f;
        _hero.StatManager.CreateOrGetStat(E_StatType.MinHealth).ModifiedValue = 0;
        _hero.StatManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue = 9798.5f;

        _hero.StatManager.CreateOrGetStat(E_StatType.AttackPoint).ModifiedValue = 860.9f;

        _hero.StatManager.CreateOrGetStat(E_StatType.CriticalRate).ModifiedValue = 21.7f/100;
        _hero.StatManager.CreateOrGetStat(E_StatType.CriticalMultiplier).ModifiedValue = 2.05f;

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
        _hero.StatManager.CreateOrGetStat(E_StatType.MinRange).ModifiedValue = 2.5f;
        _hero.StatManager.CreateOrGetStat(E_StatType.CurrentRange).ModifiedValue = 0;


        _hero.StatManager.CreateOrGetStat(E_StatType.MaxAttackSpeed).ModifiedValue = 2.5f;
        _hero.StatManager.CreateOrGetStat(E_StatType.MinAttackSpeed).ModifiedValue = 0.25f;
        _hero.StatManager.CreateOrGetStat(E_StatType.CurrentAttackSpeed).ModifiedValue = 1.4f;

        _hero.StatManager.CreateOrGetStat(E_StatType.PhysicalPenetration).ModifiedValue = 0;
        _hero.StatManager.CreateOrGetStat(E_StatType.MagicalPenetration).ModifiedValue = 0;
        //이동속도
        _hero.StatManager.CreateOrGetStat(E_StatType.MoveSpeed).ModifiedValue = 75;

        _hero.StatManager.CreateOrGetStat(E_StatType.KnockbackResistance).ModifiedValue = 0;

        _hero.StatManager.CreateOrGetStat(E_StatType.TimeAccelerationRate).ModifiedValue = 1;
        _hero.StatManager.CreateOrGetStat(E_StatType.MotionAccelerationRate).ModifiedValue = 1;
        _hero.StatManager.CreateOrGetStat(E_StatType.ScaleMultiplier).ModifiedValue = 1;
        _hero.GetComponent<Animator>().SetBool("inBattle",true);
    }

    // Use this for initialization
    void Start()
    {
        

        EnterTheBattle();
        enemyUnitList[0].StatManager.CreateOrGetStat(E_StatType.MoveSpeed).ModifiedValue = 0;
        //enemyList[0].StatManager.CreateOrGetStat(E_StatType.MaxRange).ModifiedValue = 1;
        //enemyList[0].StatManager.CreateOrGetStat(E_StatType.MinRange).ModifiedValue = 0;
        CheckingHeadUnitStart();

        leaderHero = playerUnitList[0];
        targetUnit = leaderHero;
    }


    void CheckingHeadUnitStart()
    {
        StartCoroutine(CheckingHeadUnit());
    }
    IEnumerator CheckingHeadUnit()
    {
        playerHeadUnit = playerUnitList[0];
        monsterHeadUnit = enemyUnitList[0];

        while (true)
        {
            foreach(Unit unit in playerUnitList)
            {
                if(unit.transform.position.x > playerHeadUnit.transform.position.x)
                {
                    playerHeadUnit = unit;
                }
            }
            foreach (Unit unit in enemyUnitList)
            {
                if (unit.transform.position.x < monsterHeadUnit.transform.position.x)
                {
                    playerHeadUnit = unit;
                }
            }
            yield return null;
        }
    }
}
