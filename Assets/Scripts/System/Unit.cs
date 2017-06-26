using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    Animator animator;

    StatManager statManager = new StatManager();
    BuffManager buffManager = new BuffManager();

    public StatManager StatManager
    {
        get
        {
            return statManager;
        }
    }
    public BuffManager BuffManager
    {
        get
        {
            return buffManager;
        }
    }

    public ActionQueueManager skillQueue = new ActionQueueManager();

}
public partial class Unit : MonoBehaviour
{
    //진영정보
    public E_GroupTag groupTag;

    //전투 관련
    bool inBattle;
    /// <summary>
    /// 전투 진입
    /// </summary>
    public void EnterBattle()
    {
        inBattle = true;
        foreach(Effect effect in EnterBattleEffect)
        {
            effect.ActivateEffect();
        }
    }
    /// <summary>
    /// 전투 이탈
    /// </summary>
    public void ExitBattle()
    {
        inBattle = false;
        foreach (Effect effect in ExitBattleEffect)
        {
            effect.ActivateEffect();
        }
    }
    public List<Effect> EnterBattleEffect = new List<Effect>();
    public List<Effect> ExitBattleEffect = new List<Effect>();
   
    /*
     * 사망
     * 사망시 버프,디버프 제거.
     * 상호작용 중단.
     * 사망 애니메이션 재생
     * 사망 시작 이벤트 발생
     * 사망 애니메이션 종료시 이벤트 발생
     */
    bool isDead;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
            IsNormal = (!value) & (!isGroggy);
        }
    }
    public void DeadStart()
    {
        isGroggy = false;
        IsDead = true;
        //모든 버프를 해제한다.
        //이 용사와 관계된 모든걸 해제한다.
        //사망 애니메이션 시작.
        //충돌체도 꺼야겠지?
        //아래 효과는 필요 없어 보인다. 이후 수정
        foreach (Effect effect in DeadStartEffect)
        {
            effect.ActivateEffect(this);
        }
    }
    public void DeadEnd()
    {
        foreach (Effect effect in DeadEndEffect)
        {
            effect.ActivateEffect(this);
        }
    }
    public List<Effect> DeadStartEffect = new List<Effect>();
    public List<Effect> DeadEndEffect = new List<Effect>();
   
    /// <summary>
    /// 부활
    /// </summary>
    public void Rebirth()
    {
        IsDead = false;
        //사망과 반대겠지?
    }
    public List<Effect> RebirthEffect = new List<Effect>();


    //기절 관련
    bool isGroggy;
    public bool IsGroggy
    {
        get
        {
            return isGroggy;
        }
        set
        {
            isGroggy = value;
            IsNormal = (!value)&(!isDead);
        }
    }
    /// <summary>
    /// 기절 시작
    /// </summary>
    public void GroggyStart()
    {
        IsGroggy = true;
        //시전중이던 모션 캔슬
        //그로기 애니메이션 실행
        //기절중 스킬 스택 저장여부는 확인해봐.
        foreach (Effect effect in GroggyStartEffect)
        {
            effect.ActivateEffect();
        }
    }
    /// <summary>
    /// 기절 종료
    /// </summary>
    public void GroggyEnd()
    {
        IsGroggy = false;
        //그로기 애니메이션 종료
        //달리기 모션으로 되돌림.

        foreach (Effect effect in GroggyEndEffect)
        {
            effect.ActivateEffect();
        }
    }
    public List<Effect> GroggyStartEffect = new List<Effect>();
    public List<Effect> GroggyEndEffect = new List<Effect>();
    

    //정상 여부
    bool isNormal;
    public bool IsNormal
    {
        get
        {
            return isNormal;
        }
        set
        {
            isNormal = value;
        }
    }
/*
    void RunningStart()
    {
        StartCoroutine(Running());
    }
    IEnumerator Running()
    {
        yield return null;
        Unit headEnemy;
        float enemyDistance=0;

        StatFloat moveSpeed = statManager.CreateOrGetStat(E_StatType.MoveSpeed);
        StatFloat minRange = statManager.CreateOrGetStat(E_StatType.MinRange);
        StatFloat maxRange = statManager.CreateOrGetStat(E_StatType.MaxRange);

        while (true)
        {
            if(groupTag==E_GroupTag.Player)
            {

                headEnemy = GameManager.monsterHeadUnit;
                enemyDistance = headEnemy.transform.position.x - transform.position.x;

                if (enemyDistance> minRange.ModifiedValue)
                {
                    //최소 사거리 밖일때
                    Debug.Log(1);
                    transform.position= Vector3.Lerp(transform.position, headEnemy.transform.position-new Vector3(minRange.ModifiedValue,0), Time.deltaTime * moveSpeed.ModifiedValue);
                }
                else
                {
                    //최소 사거리 안일때.
                }
            }
            else
            {

            }


            yield return null;
        }
    }


        */
    //적과 사정거리
    //사거리 관련
    E_Range enemyRange = E_Range.OutOfRange;
    E_Range deltaRange = E_Range.OutOfRange;
    float enemyDistance = 0;
    
    void RangeSearchStart()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RangeSearch());
    }
    Vector3 direction;
    IEnumerator RangeSearch()
    {
        StatFloat maxRange = StatManager.CreateOrGetStat(E_StatType.MaxRange);
        StatFloat minRange = StatManager.CreateOrGetStat(E_StatType.MinRange);

        //레이어 마스크를 지정함
        //만약에 게임에서 아군으로 만드는 기능이 있다면 이부분을 루프안으로 넣어야한다.
        LayerMask mask=~(1<<gameObject.layer);
        
        Vector2 origin;
        RaycastHit2D hit;

        //적과의 사거리

        while (name=="Player")
        {
            direction = transform.worldToLocalMatrix.MultiplyVector(transform.right);
            origin = new Vector2(transform.position.x + (0.301f * direction.x), transform.position.y + 0.1f);
            //나중에 위해서 메모를 하는건데
            //여기를 Transfrom으로 아예 저장을 해서 쉽게 처리가 가능할거같다. 한번 생각해봐라.
            //추가적으로 raycast2D를 새로 정의내려도 되고.
            if( hit = Physics2D.Raycast(origin, direction, float.MaxValue,mask) )
            {
                if (hit.transform.tag!=tag)
                {
                    enemyDistance = direction .x * (hit.transform.position.x -transform.position.x);

                    //최소 사거리 내
                    if(enemyDistance<minRange.ModifiedValue)
                    {
                        if(deltaRange!= E_Range.WithInMinRange)
                        {

                            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
                            //Debug.Log(name + " 사거리밖>> 사거리안");
                        }
                        enemyRange = E_Range.WithInMinRange;
                        deltaRange = enemyRange;
                    }//최대 사거리 밖
                    else if(enemyDistance < maxRange.ModifiedValue)
                    {
                        enemyRange = E_Range.WithInMaxRange;
                        deltaRange = enemyRange;
                    }
                    else
                    {
                        enemyRange = E_Range.OutOfRange;
                        deltaRange = enemyRange;
                    }

                    //Debug.Log(name + " find Enemy : " + hit.transform.name + "(" + enemyDistance + "m) " + enemyRange.ToString());
                }
            }
            yield return null;
        }

        //레이로 탐지
        //레인지로 가장 앞의 적과 사거리 관계를 비교하여 갱신
        //최소 사거리 밖인지
        //아니라면 최소 사거리 안?
        //둘다아니면 당연 정지
    }

    //달리기 관련 
    Rigidbody2D rigid2D;
    void RunningStart()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Running());
    }
    IEnumerator Running()
    {
        StatFloat moveSpeed = statManager.CreateOrGetStat(E_StatType.MoveSpeed);
        while (true)
        {
            if(isNormal)
            {
                if(inBattle)
                {
                    //스킬 큐가 비어있는지 체크하는 부분
                    if (skillQueue.IsEmpty)
                    {
                        //달리기 애니메이션이 미실행중이라면 애니메이션 재생
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                        {
                            if(enemyRange!=E_Range.WithInMinRange)
                            {
                                //Debug.Log(name + " "+ enemyDistance);
                                //rigid2D.velocity = direction * moveSpeed.ModifiedValue;
                                rigid2D.AddForce(direction * moveSpeed.ModifiedValue, ForceMode2D.Force);

                            }
                            else
                            {
                                //Debug.Log(name+" "+ enemyDistance);
                                //rigid2D.velocity = -direction * moveSpeed.ModifiedValue*0.5f;
                                rigid2D.AddForce(-direction * moveSpeed.ModifiedValue * 0.5f, ForceMode2D.Force);
                            }
                            /*
                            if (enemyRange == E_Range.OutOfRange)
                            {
                                if (deltaRange != E_Range.WithInMaxRange)
                                {
                                    
                                    rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
                                    Debug.Log(name + " 사거리밖>> 사거리안");
                                }
                                rigid2D.velocity = direction * moveSpeed.ModifiedValue;
                                Debug.Log(name + " 전진");
                                //rigid2D.AddForce(direction * moveSpeed.ModifiedValue, ForceMode2D.Force);
                                //Debug.Log(rigid2D.velocity);
                                //transform.Translate(direction*moveSpeed.ModifiedValue*Time.deltaTime);
                                //전진
                            }
                            else
                            {
                                if (!(moveSpeed.ModifiedValue == 0))
                                {
                                    rigid2D.velocity = -direction * moveSpeed.ModifiedValue*0.5f;
                                    Debug.Log(name+" 후진"+ rigid2D.velocity);
                                    // rigid2D.AddForce(-direction * moveSpeed.ModifiedValue * 0.5f, ForceMode2D.Force);
                                }
                                //transform.Translate(-direction * Time.deltaTime);
                                //후진
                            }*/

                        }
                    }
                }
            }
            yield return null;
        }
    }
    
    //스킬 자동 시전
    void ActionQueueCheckingStart()
    {
        StartCoroutine(ActionQueueChecking());
    }
    IEnumerator ActionQueueChecking()
    {
        string currentSkillInfo = string.Empty;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (true)
        {
            //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"));
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName(currentSkillInfo))
            {
                if (!skillQueue.IsEmpty)
                {
                    //Debug.Log(1);
                    rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
                    currentSkillInfo = skillQueue.DequeueAction().motionName;
                    animator.Play(currentSkillInfo);
                }
            }
            yield return null;

        }
    }


    //기본 공격
    public bool isAttacking = false;
    void BaseAttackCoolDownStart()
    {
        StartCoroutine(BaseAttackChecking());
    }
    IEnumerator BaseAttackChecking()
    { 
        if (skillList.Count==0)
        {
            yield break;
        }
        Skill skill = skillList[0];

        StatFloat attackSpeed = statManager.CreateOrGetStat(E_StatType.CurrentAttackSpeed);
        float currentCoolTime = 10;
        float attackPeriod = 1f;
        isAttacking = false;
        while (true)
        {
            //공격이 끝난 후부터 쿨다운 시작.
            
            if((!isAttacking) &(attackPeriod > currentCoolTime))
            {
                currentCoolTime += Time.deltaTime * attackSpeed.ModifiedValue;
            }
            else
            {
                if((enemyRange!=E_Range.OutOfRange)&(skillQueue.IsEmpty)&(!isAttacking))
                {
                    currentCoolTime = 0;
                    isAttacking = true;
                    skillQueue.AddAction(skill);
                }
            }

            yield return null;
        }
    }
    public void SetIsntAttack()
    {
        isAttacking = false;
    }
    public void SetIsAttack()
    {
        isAttacking = true;
    }
    public List<Skill> baseAttackList = new List<Skill>();



    public void GetHit(ref float damage,E_DamageType damageType,float penetrationPower)
    {
        GetDamage(damage);
        Debug.Log(name + " hit!");
    }

    public void GetDamage(float damage)
    {


        //StatManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue =
    }

    //애니메이션 트리거
    public void SkillActivate(int skillNum)
    {
        skillList[skillNum].ActivateEffect(this);
    }

    public List<Effect> effectList =new List<Effect>();
    public List<Skill> skillList = new List<Skill>();


    //n체인 스킬 사용.








    //n체인 트리거
    bool ChainTrigger1;
    bool ChainTrigger2;
    bool ChainTrigger3;
    






    /*
     * 상태 확인 [그로기, 사망이면 비정상]
     * 
     * 각각 이벤트를 갖고있다.
     * 
     * 그로기 시작
     * 그로기 중
     * 그로기 완료
     * 
     * 
     * 사망 시작
     * 사망 중
     * 사망 완료
     * 
     * 기본 공격 시작
     * 기본 공격 중
     * 기본 공격 종료
     * 
     * 
     *
     * 
     * 달리기 - 전진
     * 달리기 - 정지
     * 달리기 - 후진
     * 
     * 
     * 기본 공격 사거리 탐지 시작
     * 기본 공격 사거리 탐지 중
     * 
     * 1체인 블록 스킬 시전 시작
     * 1체인 블록 스킬 시전 중
     * 1체인 블록 스킬 시전 종료
     * 
     * 2체인 블록 스킬 시전 시작
     * 2체인 블록 스킬 시전 중
     * 2체인 블록 스킬 시전 종료
     * 
     * 3체인 블록 스킬 시전 시작
     * 3체인 블록 스킬 시전 중
     * 3체인 블록 스킬 시전 종료
     * 
     * 1체인 트리거 장전
     * 2체인 트리거 장전
     * 3체인 트리거 장전
     * 
     * 블록 사용
     * 
     * 
     * 
     * 
     * 
     * 패시브 시작
     * 패시브 중지
     * 
     * 
     * 
     * 
     * 상호 작용 시작
     * 상호 작용 중지
     * 
     * 
     * 넉백 시작
     * 넉백 중
     * 넉백 종료
     * 
     * 
     * 사망 조건 체크
     * 
     * 
     * 전투 진입
     * 전투 중
     * 전투 이탈
     * 
     * 
     * 피격 당함
     * 피해 입음
     * 
     * 회복 받음
     * 
     * 스테이터스 변화
     * 
     * 
     * SP 상승
     * SP 감소
     * 
     * 회피 체크
     * 
     * 명중 체크
     * 
     * 치명타 체크
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 각종 이벤트를 아래 기술
     * 
     * 그로기 시작 이벤트
     * 그로기 종료 이벤트
     * 
     * 사망 시작 이벤트
     * 사망 종료 이벤트
     * 
     * 기본 공격 시작 이벤트
     * 기본 공격 종료 이벤트
     * 
     * 달리기 - 전진 이벤트
     * 달리기 - 정지 이벤트
     * 달리기 - 후진 이벤트
     * 
     * 넉백 시작 이벤트
     * 넉백 종료 이벤트
     * 
     * 1체인 스킬 시전 시작 이벤트
     * 1체인 스킬 시전 종료 이벤트
     * 2체인 스킬 시전 시작 이벤트
     * 2체인 스킬 시전 종료 이벤트
     * 3체인 스킬 시전 시작 이벤트
     * 3체인 스킬 시전 종료 이벤트
     * 
     * 
     * 1체인 트리거 이벤트
     * 2체인 트리거 이벤트
     * 3체인 트리거 이벤트
     * 
     * 블록 사용 이벤트
     * 3체인 트리거 이벤트
     * 
     * 회피 이벤트
     * 명중 이벤트
     * 
     * 
     * 전투 진입 이벤트
     * 전투 이탈 이벤트
     * 
     * 
     * 캐릭터 부활 이벤트
     * 
     * 피격 이벤트
     * 회복 이벤트
     * 
     * 스테이터스 변화 이벤트
     * 
     * SP 상승 이벤트
     * SP 감소 이벤트
     * 
     * 감속 이벤트
     * 
     * 치명타 발동 이벤트
     * 
     * 
     */











    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        RangeSearchStart();
        RunningStart();
        IsNormal = true;
        inBattle = true;
        ActionQueueCheckingStart();
        BaseAttackCoolDownStart();
    }

}
//디버그
public partial class Unit : MonoBehaviour
{
    public void TestSkillAdd(string skillName)
    {
        Skill skill = new Skill();
        skill.motionName = skillName;
        skillQueue.AddAction(skill);
    }
    public void ShowAllStat()
    {
        foreach (E_StatType _statType in Enum.GetValues(typeof(E_StatType)))
        {
            Debug.Log(statManager.Get_Stat(_statType).StatName + "/" + statManager.Get_Stat(_statType).StatType + "/" + statManager.Get_Stat(_statType).ModifiedValue);
        }
    }
}
