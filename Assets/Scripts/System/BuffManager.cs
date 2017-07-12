using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    Dictionary<string, Buff> idDictionary = new Dictionary<string, Buff>();
    Dictionary<string, List<Buff>> nameDictionary = new Dictionary<string, List<Buff>>();
    Dictionary<E_BuffOrder, List<Buff>> orderDictionary = new Dictionary<E_BuffOrder, List<Buff>>();
   
    /// <summary>
    /// Id, buff Dictionary
    /// </summary>
    public Dictionary<string, Buff> BuffIdDictionary
    {
        get
        {
            return idDictionary;
        }
    }
    int idCount = 0;

    /*
    * bool Contain(id)
    * bool Contain(name)
    * bool Contain(type)
    * Create //버프를 생성
    * public Add//있으면 중첩, 없으면 생성.
    * public Remove//아예 소멸
    
    * Refresh//중첩시 새로고침효과 발동
    * Overlap//중첩 규칙에 따른 중첩
    */



    public bool Contain(string name)
    {
        if (nameDictionary.ContainsKey(name))
        {
            return nameDictionary[name].Count != 0;
        }
        else
        {
            return false;
        }
    }
    public bool Contain(E_BuffOrder orderValue)
    {
        if(orderDictionary.ContainsKey(orderValue))
        {
            return orderDictionary[orderValue].Capacity != 0;
            
        }
        else
        {
            return false;
        }
    }

    Buff Create(Buff buff, Unit caster, Unit target)
    {
        /*Buff tempBuff = MonoBehaviour.Instantiate(buff);
        tempBuff.Caster = caster;
        tempBuff.ApplyTarget = applyTarget;
        buffIdDictionary.Add(tempBuff.Id, tempBuff);
        buffIndexList.Add(tempBuff);
        tempBuff.Activate();*/
        //버프를 처음 등록한다.
        //신규 버프 생성

        Buff tempBuff = MonoBehaviour.Instantiate(buff);
        tempBuff.caster = caster;
        tempBuff.target = target;
        tempBuff.id = tempBuff.name + "_" + tempBuff.caster.ToString() + "_" + idCount;
        idCount++;

        idDictionary.Add(tempBuff.id, tempBuff);

        if (!nameDictionary.ContainsKey(tempBuff.name))
        {
            nameDictionary.Add(buff.name, new List<Buff>());
        }
        nameDictionary[tempBuff.name].Add(tempBuff);

        if(!orderDictionary.ContainsKey(tempBuff.buffOrder))
        {
            orderDictionary.Add(tempBuff.buffOrder,new List<Buff>());
        }
        orderDictionary[tempBuff.buffOrder].Add(tempBuff);

        return tempBuff;
    }
    public Buff Add(Buff buff, Unit caster, Unit target)
    {
        if(nameDictionary.ContainsKey(buff.name))
        {

        }
        else
        {
            Create(buff, caster, target);
        }

        /*
         * Buff 임시버프
         * 
         * 만약 버프가 존재하지 않다면
         *     버프 생성
         * 존재하면
         *     버프 중첩 가능하면
         *         시전자 구분해야하면
         *             시전자
         *         
         *     버프 중첩 불가능하면
         *         새로운 버프 생성
         *     
         *         
         * 
         */ 

        return buff;
    }
    public void Remove(Buff buff, Unit caster, Unit target)
    {
    }
    void Refresh(Buff buff, Unit caster, Unit target)
    {

    }
    void Overlap(Buff buff, Unit caster, Unit target)
    {

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
