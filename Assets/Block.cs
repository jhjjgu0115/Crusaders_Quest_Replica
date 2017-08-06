using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public E_SkillType skillType;
    public List<Block> linkedBlockList = new List<Block>(2);
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
        canUse = false;
        this.targetUnit = targetUnit;
        this.chainLevel = chainLevel;
        this.skillType = skillType;
        headBlock = this;
    }
    private void Start()
    {
        linkedBlockList.Add(null);
        linkedBlockList.Add(null);
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

    public void StartDropDown()
    {
        canUse = false;
        StartCoroutine(DropDown());
    }
    IEnumerator DropDown()
    {
        while (Vector3.Distance(transform.position, dropDownTargetTransform.transform.position) >= 0.4f)
        {
            transform.position = Vector3.Lerp(transform.position, dropDownTargetTransform.position, 0.3f);
            yield return null;
        }
        transform.position = dropDownTargetTransform.position;
        canUse = true;
        yield return null;
    }
    IEnumerator TryCombine()
    {
        while(true)
        {
            if(canUse)
            {

            }
            yield return null;
        }
    }
}
