using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Projectile : MonoBehaviour
{
    public Unit caster;
    public Unit target;

    //-1은 무제한.
    public int maxPenetrationCount=0;   //최대 관통 횟수.0이면 착탄즉시 터짐
    int currentPenetrationCount = 0;    //현재 관통 횟수

    /// <summary>
    /// 투사체 지속시간. -1이면 무제한 비행.
    /// </summary>
    public float durationTime;      //유지 시간
    float currentRemainTime;        //남은 시간
    /// <summary>
    /// 반복 주기
    /// </summary>
    public float reiterationPeriod; //반복 주기
    float currentReiterationPeriod; //현재 반복 주기값
    /// <summary>
    /// 반복 횟수. -1이면 제한 없음
    /// </summary>
    public int repeatCount;         //반복 횟수
    int currentRepeatCount;         //현재 횟수

    public Model penetrationVFXModel;
    public Model destroyModel;


    public List<Effect> createEffectList = new List<Effect>();
    public List<Effect> periodEffectList = new List<Effect>();
    public List<Effect> durationEndEffectList = new List<Effect>();
    public List<Effect> penetrationEffectList = new List<Effect>();
    public List<Effect> destroyEffectList = new List<Effect>();
}
public partial class Projectile : MonoBehaviour
{
    public void Initialize(Unit _caster)
    {
        caster = _caster;
    }

    public void OnCreat()
    {
        RefreshEffectCasterBasedOnly(createEffectList);
        RefreshEffectCasterBasedOnly(periodEffectList);
        RefreshEffectCasterBasedOnly(durationEndEffectList);
        RefreshEffectCasterBasedOnly(penetrationEffectList);
        RefreshEffectCasterBasedOnly(destroyEffectList);
        //모든 효과 시전자 기반 갱신.
        CreatEffectActivate();
    }
    public void FlyingStart()
    {
        StartCoroutine(OnFlying());
    }
    IEnumerator OnFlying()
    {
        currentReiterationPeriod = 0;
        currentRepeatCount = 0;
        if (durationTime >= 0)    //일정 시간동안 지속
        {
            currentRemainTime = durationTime;
            while (true)
            {

                //지속시간이 존재할때.
                if (currentRemainTime >= 0)
                {
                    currentRemainTime -= Time.deltaTime/*X시간 가속 배율*/;

                    //주기 계산
                    if (currentReiterationPeriod < reiterationPeriod)//주기가 차지 않으면 채운다.
                    {
                        currentReiterationPeriod += Time.deltaTime/*X시간 가속 배율*/;
                    }
                    else//주기가 가득참
                    {
                        if (repeatCount == -1)//무제한 발생
                        {
                            //효과 발생
                            PeriodEffectActivate();
                        }
                        else if (currentRepeatCount < repeatCount - 1)//반복 횟수 제한
                        {
                            //효과발생
                            PeriodEffectActivate();
                            currentRepeatCount += 1;
                        }
                        else//반복 횟수를 초과
                        {
                            break;
                        }
                        currentReiterationPeriod -= reiterationPeriod - (Time.deltaTime/*X시간 가속 배율*/);
                    }
                }
                else//지속시간이 만료되었을때.
                {
                    Debug.Log(1);
                    break;
                }

                yield return null;
            }
            //마지막 효과 발동
            PeriodEffectActivate();
            //만료 효과 발생
            DurationEndEffectActivate();
        }
        else                     //영구 지속
        {
            while (true)
            {

                //주기 계산
                if (currentReiterationPeriod < reiterationPeriod)//주기가 차지 않으면 채운다.
                {
                    currentReiterationPeriod += Time.deltaTime /*X시간 가속 배율*/;
                }
                else//주기가 가득참
                {
                    if (repeatCount == -1)//무제한 발생
                    {
                        //효과 발생
                        PeriodEffectActivate();
                    }
                    else if (currentRepeatCount < repeatCount - 1)//반복 횟수 제한
                    {
                        //효과발생
                        PeriodEffectActivate();
                        currentRepeatCount += 1;
                    }
                    else//반복 횟수를 초과
                    {
                        break;
                    }
                    currentReiterationPeriod -= reiterationPeriod - (Time.deltaTime /*X시간 가속 배율*/);
                }

                yield return null;
            }
            //마지막 효과 발동
            PeriodEffectActivate();
        }

        Destroy(gameObject);
        yield return null;
    }
    void FlyingEnd()
    {
    }
    void OnDestroy()
    {
        DestroyEffectActivate();
    }
    public void OnPenetration()
    {
        //충돌 횟수 무제한.
        if(maxPenetrationCount==-1)
        {
            if (penetrationVFXModel)
            {
                Instantiate(penetrationVFXModel).transform.position = transform.position;
            }
            RefreshEffectTargetBasedOnly(penetrationEffectList);
            penetrationEffectActivate();
            target.EndCollision(this);
            currentPenetrationCount++;
        }
        else
        {
            if (currentPenetrationCount < maxPenetrationCount)
            {
                if (penetrationVFXModel)
                {
                    Instantiate(penetrationVFXModel).transform.position = transform.position;
                }
                RefreshEffectTargetBasedOnly(penetrationEffectList);
                penetrationEffectActivate();
                target.EndCollision(this);
                currentPenetrationCount++;
            }
            else
            {
                if (destroyModel)
                {
                    Instantiate(destroyModel).transform.position = transform.position;
                }
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                RefreshEffectTargetBasedOnly(destroyEffectList);
                DestroyEffectActivate();
                target.EndCollision(this);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        target = other.GetComponent<Unit>();
        target.StartCollision(this);
    }




    void ActivateAllEffectInList(List<Effect> effectList)
    {
        for (int index = 0; index < effectList.Count; index++)
        {
            effectList[index].ActivateEffect(caster);
        }
    }
    
    void CreatEffectActivate()
    {
        ActivateAllEffectInList(createEffectList);
    }
    void DurationEndEffectActivate()
    {
        ActivateAllEffectInList(durationEndEffectList);
    }
    void DestroyEffectActivate()
    {
        ActivateAllEffectInList(destroyEffectList);
    }
    void PeriodEffectActivate()
    {
        ActivateAllEffectInList(periodEffectList);
    }
    void penetrationEffectActivate()
    {
        ActivateAllEffectInList(penetrationEffectList);
    }
    
    void RefreshEffectCasterBasedOnly(List<Effect> effectList)
    {
        for (int index = 0; index < effectList.Count; index++)
        {
            effectList[index].RefreshCasterBasedAmount(caster);
        }
    }
    void RefreshEffectTargetBasedOnly(List<Effect> effectList)
    {
        for (int index = 0; index < effectList.Count; index++)
        {
            effectList[index].RefreshTargetBasedAmount(target);
        }
    }
    void RefreshEffectAll(List<Effect> effectList)
    {
        for (int index = 0; index < effectList.Count; index++)
        {
            effectList[index].RefreshAllAmount(caster, target);
        }
    }
}
public partial class Projectile : MonoBehaviour
{
}
