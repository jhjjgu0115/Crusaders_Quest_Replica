using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    Dictionary<string, Buff> idDictionary = new Dictionary<string, Buff>();
    Dictionary<string, List<Buff>> nameDictionary = new Dictionary<string, List<Buff>>();
    Dictionary<string, List<Buff>> casterDictionary = new Dictionary<string, List<Buff>>();
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

        if (!orderDictionary.ContainsKey(tempBuff.buffOrder))
        {
            orderDictionary.Add(tempBuff.buffOrder,new List<Buff>());
        }
        orderDictionary[tempBuff.buffOrder].Add(tempBuff);

        return tempBuff;
    }

    public Buff Add(Buff buff, Unit caster, Unit target)
    {
        if (nameDictionary.ContainsKey(buff.name))//해당 버프가 걸린적이 있는가?
        {   //걸린적이 있어 버프[이름]컨테이너는 있다.
            if(nameDictionary[buff.name].Count==0)//해당 이름의 버프가 있는가?
            {   //없다.
                 
                return Create(buff, caster, target);//그럼 새로 하나 인스턴스를 만들어서
                //이름 딕셔너리에 등록하고
                //끝낸다.
            }
            else
            {   //해당 이름의 버프가 하나이상 존재.
                if (buff.canOverlap)//그럼 중첩 가능해?
                {   //중첩 가능하다.
                    if (buff.isSeparateCaster)//시전자를 구분해야하나?
                    {
                        //시전자 구분을 해야한다면
                        foreach (Buff _buff in nameDictionary[buff.name])
                        {
                            if (_buff.caster == buff.caster)//순회하며 해당 캐스터가 있는지 확인하고
                            {
                                //있다면 해당 버프에다가 중첩
                                Overlap(_buff, caster, target);
                                //그리고 끝낸다.
                                return _buff;
                            }
                        }
                        return Create(buff, caster, target);
                        //빠져나와버렸다면 버프가 없는것.
                        //버프를 신규 작성한다.
                        //등록하고
                        //끝낸다.
                    }
                    else
                    {//시전자 구분 안한다면
                        //그냥 이름[0]번째 버프에다 중첩을 시켜버린다.
                        Overlap(nameDictionary[buff.name][0], caster, target);
                       
                    }
                }
                else
                {   //중첩이 불가능하다.
                    return Create(buff, caster, target); ;//신규작성하고
                    //이름에 등록하고
                    //끝낸다.
                }
            }
        }
        else
        {
            return Create(buff,caster,target);
        }
        return null;
    }
    public void Remove(Buff buff, Unit caster, Unit target)
    {
    }
    void Refresh(Buff buff, Unit caster, Unit target)
    {

    }
    void Overlap(Buff buff, Unit caster, Unit target)
    {
        buff.OverlapBuff();
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
