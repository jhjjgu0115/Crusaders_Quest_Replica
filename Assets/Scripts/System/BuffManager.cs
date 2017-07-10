using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    Dictionary<string, Buff> buffDictionary = new Dictionary<string, Buff>();
    List<Buff> positiveBuffList = new List<Buff>();
    List<Buff> nagativeBuffList = new List<Buff>();
    /// <summary>
    /// Id, buff Dictionary
    /// </summary>
    public Dictionary<string, Buff> BuffIdDictionary
    {
        get
        {
            return buffDictionary;
        }
    }
    /// <summary>
    /// 버프 리스트
    /// </summary>

    /*
    * bool Contain(id)
    * bool Contain(name)
    * bool Contain(type)
    * Create //버프를 생성
    * public Add//있으면 중첩, 없으면 생성.
    * public Remove//아예 소멸
    * 
    * Refresh//중첩시 새로고침효과 발동
    * Overlap//중첩 규칙에 따른 중첩
    */

    public bool Contain(string name)
    {
        return buffDictionary.ContainsKey(name);
    }
    public bool Contain(E_BuffOrder orderValue)
    {
        if(orderValue==E_BuffOrder.positive)
        {
            if (positiveBuffList.Count != 0)
                return true;
            else
                return false;
        }
        else
        {
            if (nagativeBuffList.Count != 0)
                return true;
            else
                return false;
        }
    }

    Buff Create(Buff buff)
    {
        return buff;
    }
    Buff Add(Buff buff)
    {
        Buff tempBuff;
        if(Contain(buff.name))
        {
            if(buffDictionary[buff.name])
            {

            }
        }
        else
        {
            tempBuff=Create(buff);
        }

        return buff;
    }











    /*


/// <summary>
/// Id를 이용하여 버프를 획득한다.
/// </summary>
/// <param name="id">찾을 버프의 id값</param>
/// <returns></returns>
public Buff GetBuff(string id)
{
    if (Contains(id))
    {
        return buffIdDictionary[id];
    }
    return null;
}
/// <summary>
/// id값으로 버프를 생성함.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="id"></param>
/// <returns></returns>
public void Create(Buff buff, Unit caster, Unit applyTarget)
{
    Buff tempBuff = MonoBehaviour.Instantiate(buff);
    tempBuff.Caster = caster;
    tempBuff.ApplyTarget = applyTarget;
    buffIdDictionary.Add(tempBuff.Id, tempBuff);
    buffIndexList.Add(tempBuff);
    tempBuff.Activate();
}
/// <summary>
/// 해당 id의 버프가 존재하는지 확인
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
public bool Contains(string id)
{
    return buffIdDictionary.ContainsKey(id);
}
/// <summary>
/// 해당 id의 버프가 존재하는지 확인
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
public bool Contains(Buff buff)
{
    return buffIdDictionary.ContainsValue(buff);
}
/// <summary>
/// 해당 id의 버프를 중첩을 쌓는다.
/// </summary>
/// <param name="id"></param>
public void Overlap(string id)
{
    GetBuff(id).OverlapBuff();
}
/// <summary>
/// 해당 id의 버프를 새로고침한다.
/// </summary>
/// <param name="id"></param>
public void Refresh(string id)
{
    GetBuff(id).RefreshBuff();
}

/// <summary>
/// 해당 id의 버프 소멸
/// </summary>
/// <param name="id"></param>
public void RemoveBuff(string id)
{
    if (Contains(id))
    {
        Buff temp_Buff = GetBuff(id);
        buffIdDictionary.Remove(id);
        buffIndexList.Remove(temp_Buff);
        temp_Buff.DestroyBuff();
    }
}
/// <summary>
/// 순차적으로 해제 가능한 버프 삭제.
/// </summary>
/// <param name="id"></param>
public void RemoveBuff()
{
    for (int index = BuffIndexList.Count - 1; 0 <= index; index--)
    {
        //if (!buffIndexList[index].CanPurify)
        //{
            Buff temp_Buff = buffIndexList[index];
            buffIdDictionary.Remove(buffIndexList[index].Id);
            buffIndexList.Remove(temp_Buff);
            temp_Buff.DestroyBuff();
            break;
        //}
    }
}

/// <summary>
/// 버프 생성 혹은 중첩
/// </summary>
/// <param name="buff"></param>
public void CreateOrOverLap(Buff buff, Unit caster, Unit applyTarget)
{
    if (Contains(buff.Id))
    {
        Overlap(buff.Id);
    }
    else
    {
        Create(buff, caster, applyTarget);
    }
}*/
}
