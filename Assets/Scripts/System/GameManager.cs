using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public List<Unit> playerUnitList = new List<Unit>();
    public List<Unit> alivePlayerUnitList = new List<Unit>();
    public List<Unit> deadPlayerUnitList = new List<Unit>();
    public List<Unit> enemyUnitList = new List<Unit>();
}
    public partial class GameManager : MonoBehaviour
{
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
    public List<Unit> PlayerUnitList
    {
        get
        {
            return playerUnitList;
        }
    }


    Unit leaderHero;

    //플레이어 유닛 리스트
    //웨이브 정보
    //웨이브 세트 리스트<몬스터 세트>
    //리스트<몬스터> 각각의 몬스터 프리펩 


    //각종 용사 리스트에 대한 쿼리들
    //------------------------------
    //용사 쿼리종류
    public Unit GetAliveHeadHero
    {
        get
        {
            Unit hero = alivePlayerUnitList[0];
            for (int index = 1; index < alivePlayerUnitList.Count; index++)
            {
                if (hero.transform.position.x < alivePlayerUnitList[index].transform.position.x)
                {
                    hero = alivePlayerUnitList[index];
                }
            }

            return hero;
        }
    }
    public Unit GetAliveTailHero
    {
        get
        {
            Unit hero = alivePlayerUnitList[0];
            for (int index = 1; index < alivePlayerUnitList.Count; index++)
            {
                if (hero.transform.position.x > alivePlayerUnitList[index].transform.position.x)
                {
                    hero = alivePlayerUnitList[index];
                }
            }

            return hero;
        }

    }
    public Unit GetDeadHeadHero
    {
        get
        {
            Unit hero = deadPlayerUnitList[0];
            for (int index = 1; index < deadPlayerUnitList.Count; index++)
            {
                if (hero.transform.position.x < deadPlayerUnitList[index].transform.position.x)
                {
                    hero = deadPlayerUnitList[index];
                }
            }

            return hero;
        }
    }
    public Unit GetDeadTailHero
    {
        get
        {
            Unit hero = deadPlayerUnitList[0];
            for (int index = 1; index < deadPlayerUnitList.Count; index++)
            {
                if (hero.transform.position.x > deadPlayerUnitList[index].transform.position.x)
                {
                    hero = deadPlayerUnitList[index];
                }
            }

            return hero;
        }

    }
    public Unit GetCurrentLeaderHero
    {
        get
        {
            return leaderHero;
        }
    }
    public Unit GetRandomAliveHero
    {
        get
        {
            List<Unit> tempHeroList = new List<Unit>();
            for(int index=0;index<playerUnitList.Count;index++)
            {
                if(playerUnitList[index].IsAlive)
                {
                    tempHeroList.Add(playerUnitList[index]);
                }

            }


            Unit hero = playerUnitList[Random.Range(0, 3)];
            return hero;
        }
    }
    void ResetLeaderHero()
    {
        leaderHero = GetAliveHeadHero;
    }

    void HeroDead(Unit deadUnit)
    {
        alivePlayerUnitList.Remove(deadUnit);
        deadPlayerUnitList.Add(deadUnit);

        if(alivePlayerUnitList.Count==3)
        {
            //게임오버
        }
    }
    void HeroRebirth(Unit aliveUnit)
    {
        alivePlayerUnitList.Add(aliveUnit);
        deadPlayerUnitList.Remove(aliveUnit);
    }


    //Get 리더 용사
    //Get 살아있는 모든 용사들
    //Get 죽어있는 모든 용사들
    //Get 죽어있는 용사중 선두 용사
    //Get 죽어있는 용사중 후미 용사
    //Get Random 용사

    //몬스터 쿼리 종류
    //Get 맨앞 몬스터
    //Get 제일 후열 몬스터


    //웨이브 관리
    //----------
    //웨이브 시작(N)
    //웨이브 전체 루프관리 
    //


    //플레이어 승리 모든 웨이브 종료
    //플레이어 패배 아군 생존 0 일때
    
    //웨이브 정보

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
        foreach (E_StatType _statType in System.Enum.GetValues(typeof(E_StatType)))
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

    public List<string> playerHeroIDList = new List<string>(3);

    public void ReadPlayerData()
    {


    }


    // Use this for initialization
    void Start()
    {
        EnterTheBattle();
        enemyUnitList[0].StatManager.CreateOrGetStat(E_StatType.MoveSpeed).ModifiedValue = 0;
        enemyUnitList[1].StatManager.CreateOrGetStat(E_StatType.MoveSpeed).ModifiedValue = 0;
        //enemyList[0].StatManager.CreateOrGetStat(E_StatType.MaxRange).ModifiedValue = 1;
        //enemyList[0].StatManager.CreateOrGetStat(E_StatType.MinRange).ModifiedValue = 0;

        leaderHero = playerUnitList[0];
    }

    


}
