using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    List<Block> linkedBlockList = new List<Block>(2);
    int chainLevel = 0;
    Unit targetUnit = null;
    public bool canUse = false;

    public void Initialize(int chainLevel,Unit targetUnit)
    {
        canUse = false;
        this.targetUnit = targetUnit;
        this.chainLevel = chainLevel;
        linkedBlockList.Add(null);
        linkedBlockList.Add(null);
    }

    void UseBlock()
    {
        Debug.Log(targetUnit + " - " + chainLevel + "체인 시전");
        BlockManager.Instance.lastBlockIndex--;
        BlockManager.Instance.ReturnBlockPool(this);
        //해당 체인레벨로 유닛에게 스킬 시전을 요청.
    }


    private void OnMouseDown()
    {
        if(canUse)
        {
            UseBlock();
        }
        //연결 블록에 대한 블록매니저에 소멸 요청. 
        //블록 소멸 요청
    }

}
