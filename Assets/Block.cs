using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public E_SkillType skillType;
    public Block headBlock = null;
    public int chainLevel = 0;
    public Unit targetUnit = null;
    public bool canUse = false;

    Transform dropDownTargetTransform;
    public Transform DropDownTargetTransform
    {
        get
        {
            return dropDownTargetTransform;
        }
        set
        {
            dropDownTargetTransform = value;
        }
    }
    public void SetBlockData(int chainLevel,Unit targetUnit,E_SkillType skillType)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(BlockManager.Instance.GetComponent<RectTransform>().rect.width, 0);
        canUse = true;
        this.targetUnit = targetUnit;
        this.chainLevel = chainLevel;
        this.skillType = skillType;
        headBlock = this;
        //디버그
        if(targetUnit)
        {
            if (targetUnit.name == "Player")
                GetComponent<Image>().color = Color.yellow;
            else
                GetComponent<Image>().color = Color.green;
        }
    }
    private void Start()
    {
    }
    private void OnMouseDown()
    {
        if(canUse)
        {
            BlockManager.Instance.BlockUse(this);
        }
        //연결 블록에 대한 블록매니저에 소멸 요청. 
        //블록 소멸 요청
    }

    /// <summary>
    /// 블록 드롭 시작.
    /// </summary>
    public void StartDropDown()
    {
        if (canUse)
        {
            StartCoroutine(DropDown());
        }
    }
    IEnumerator DropDown()
    {
        canUse = false;
        while (Vector3.Distance(transform.position, dropDownTargetTransform.transform.position) >= 0.4f)
        {
            transform.position = Vector3.Lerp(transform.position, dropDownTargetTransform.position, 0.4f);
            yield return null;
        }
        transform.position = dropDownTargetTransform.position;
        canUse = true;
    }
    public void StartTryCombine(params Block[] blocks)
    {
        StopCoroutine(TryCombine());
        StartCoroutine(TryCombine(blocks));
    }

    IEnumerator TryCombine(params Block[] blocks)
    {
        List<Block> blockPanel = BlockManager.Instance.blockPanel;

        while (!canUse)
        {
            yield return null;
        }
        BlockManager.Instance.Combine(blocks);
    }

    public void StartDropCombine()
    {
        StartCoroutine(DropDownCombine());
    }
    IEnumerator DropDownCombine()
    {
        while (!canUse)
        {
            yield return null;
        }
        List<Block> blockPanel = BlockManager.Instance.blockPanel;
        int thisBlockIndex = blockPanel.IndexOf(this);
        //이 블록이 맨앞 인덱스가 아니며
        if (thisBlockIndex > 0)
        {
            //그 앞의 블록이 있되 3체인 미만이며
            if (blockPanel[thisBlockIndex - 1].chainLevel < 3)
            {
                //이블럭과 앞블럭의 헤더가 동일 블럭이면
                if ((blockPanel[thisBlockIndex - 1].targetUnit == targetUnit) && (blockPanel[thisBlockIndex - 1].skillType == skillType))
                {
                    BlockManager.Instance.Combine(blockPanel.GetRange(blockPanel.IndexOf(blockPanel[thisBlockIndex - 1].headBlock), blockPanel[thisBlockIndex - 1].chainLevel + 1).ToArray());
                }

            }

        }
    }
}
