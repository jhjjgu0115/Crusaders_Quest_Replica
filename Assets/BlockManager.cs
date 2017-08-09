using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    public Block block;
    static BlockManager instance = null;
    public static BlockManager Instance
    {
        get
        {
            if (instance)
            {
                return instance;
            }
            else
            {
                instance = FindObjectOfType<BlockManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "GameManager";
                    instance = container.AddComponent<BlockManager>();
                }
                return instance;
            }
        }
    }
    List<Unit> playerUnitList = null;
    List<int> dropTable = new List<int>(); 

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
    void Start()
    {
        InitializeBlockPool();
        playerUnitList = GameManager.Instance.PlayerUnitList;
        StartGenerateBlock();
        dropTable.Add(1);
        dropTable.Add(1);
        dropTable.Add(0);
        dropTable.Add(1);

        dropTable.Add(1);
        dropTable.Add(1);
        dropTable.Add(1);
        dropTable.Add(0);
    }

    /*
     * 블록 풀 생성
     * 블록 풀 초기화
     * 블록 풀에서 꺼내기
     * 블록 풀에 반납(반납할 블록)
     */
    List<Block> blockPool;
    void InitializeBlockPool()
    {
        blockPool = new List<Block>();
        block = Resources.Load<Block>("Prefabs/System/BlockPanel/Block");
        lastBlockNextIndex = 0;
        for (int index = 0; index < 8; index++)
        {
            blockPool.Add(Instantiate<Block>(block));
            blockPool[index].transform.SetParent(transform, false);
            blockPool[index].SetBlockData(0, null,E_SkillType.Normal);
            blockPool[index].gameObject.SetActive(false);
        }
    }
    Block Pop()
    {
        foreach (Block block in blockPool)
        {
            if (!block.gameObject.activeInHierarchy)
            {
                block.gameObject.SetActive(true);
                return block;
            }
        }
        return null;
    }
    void Push(Block block)
    {
        block.gameObject.SetActive(false);
    }

    /* 
     * 블록 떨구기(떨굴 위치,떨구기
     * 블록 합치기
     * 블록 자동 생성 시작
     * 블록 자동 생성 중지
     */
    public List<Block> blockPanel = new List<Block>();
    public int lastBlockNextIndex = 0;
    public List<Transform> blockPanelPostionList = new List<Transform>();

    void StartGenerateBlock()
    {
        StartCoroutine(GenerateBlock());
    }
    void StopGenerateBlock()
    {
        StopCoroutine(GenerateBlock());
    }
    //블록 주기 생성
    public float blockGeneratePeriod = 0;
    float currentGeneratingPeriod = 0;
    IEnumerator GenerateBlock()
    {
        while (true)
        {
            if (lastBlockNextIndex < 8)
            {
                if (currentGeneratingPeriod < blockGeneratePeriod)
                {
                    currentGeneratingPeriod += Time.deltaTime;
                }
                else
                {
                    currentGeneratingPeriod = 0;
                    Block tempBlock = Pop();
                    Unit randomUnit;
                    
                    if (dropTable[blockPanel.Count] ==0)
                        randomUnit= GameManager.Instance.PlayerUnitList[0];
                    else
                        randomUnit= GameManager.instance.PlayerUnitList[1];


                    tempBlock.SetBlockData(1, randomUnit, E_SkillType.Normal);
                    tempBlock.DropDownTargetTransform = blockPanelPostionList[lastBlockNextIndex];
                    blockPanel.Add(tempBlock);
                    tempBlock.StartDropDown();
                    //첫 인덱스가 아니고
                    if (lastBlockNextIndex!=0)
                    {
                        //3체인이 아니며
                        if(blockPanel[lastBlockNextIndex-1].chainLevel!=3)
                        {
                            //동일 블럭이면
                            if ((blockPanel[lastBlockNextIndex - 1].targetUnit == blockPanel[lastBlockNextIndex].targetUnit) & (blockPanel[lastBlockNextIndex - 1].skillType == blockPanel[lastBlockNextIndex].skillType))
                            {
                                Debug.Log(" : ");
                                tempBlock.StartTryCombine(blockPanel.GetRange(blockPanel.IndexOf(blockPanel[lastBlockNextIndex - 1].headBlock), blockPanel[lastBlockNextIndex - 1].chainLevel + 1).ToArray());
                            }

                        }

                    }
                    lastBlockNextIndex++;

                }
            }
            else
            {
                currentGeneratingPeriod = 0;
            }


            yield return null;
        }
    }

    //재배열만 해준다.
    public void Combine(params Block[] blocks)
    {
        int headIndex = 0;                  //맨앖
        int headIndexChangeCount = 1;       //1
        int remainBlocks = blocks.Length;   //7
        int remainBlocksChainLevel = blocks.Length % 3;
        
        for (int index = 0; index < blocks.Length; index++)
        {

            if (!blocks[index].gameObject.activeInHierarchy)
            {
                break;
            }
            //헤더블록은 현재의 헤더 블록
            blocks[index].headBlock = blocks[headIndex];    //0번째

            //체인수설정
            //재설정될 블록이 3개 이상이라면
            if (remainBlocks>=3)
            {
                blocks[index].chainLevel = 3;
                //blocks[index].gameObject.GetComponent<Image>().color = Color.green;
            }
            else
            {
                blocks[index].chainLevel = remainBlocksChainLevel;

                if(remainBlocksChainLevel == 2)
                {
                   //blocks[index].gameObject.GetComponent<Image>().color = Color.yellow;
                }

            }
            //헤더블록 재설정
            if (headIndexChangeCount == 3)
            {
                headIndex += 3;
                headIndexChangeCount = 1;
                remainBlocks -= 3;
            }
            else
            {
                headIndexChangeCount++;

            }
        }
    }
    public void BlockUse(Block block)
    {
        //블록헤더의 정보를 토대로 시전자에게 스킬 인자 전달.
        //블록헤더의 체인값만큼 이후열의 블록들 소멸 실시
        //체인값 이후 열부터 드롭다운 실시
        //드롭다운이 완료되면 앞과 뒤를 비교하여 같을경우 콤바인 진행
        //
        int usingBlockIndex = blockPanel.IndexOf(block);
        Block headBlock = block.headBlock;              //사용된 블록의 헤더 블록
        int usingBlockHeadIndex = blockPanel.IndexOf(headBlock);//사용된 블록의 헤더블록의 인덱스
        int usingChain = block.chainLevel;              //사용된 블록의 체인수

        block.targetUnit.skillQueue.AddAction(block.targetUnit.skillList[usingChain-1]);
        /*
         * headBlock.TargetUnit.SkillUse(N체인);
         */
        //블록 풀에 사용한 블록만큼 반환한다.
        //헤더 블록을 기준으로 체인갯수만큼 반환
        foreach (Block usingBlock in blockPanel.GetRange(usingBlockHeadIndex,usingChain))
        {
            Push(usingBlock);
        }
        blockPanel.RemoveRange(usingBlockHeadIndex, usingChain);

        //드랍이 가능한 마지막 인덱스를 체인값만큼 빼서 재설정.
        lastBlockNextIndex -= usingChain;
        //당겨진 블록 위치부터 


        int dropDownBlockCount = blockPanel.Count - usingBlockHeadIndex;
        for (int index = usingBlockHeadIndex; index< usingBlockHeadIndex + dropDownBlockCount; index++)
        {
            blockPanel[index].DropDownTargetTransform = blockPanelPostionList[index];
            blockPanel[index].StartDropDown();
        }
        
        if (usingBlockHeadIndex != 0)
        {
            //사용된 블록의 앞에 체인의 합이 3이면 합칠 필요가 없다.
            if (blockPanel[usingBlockHeadIndex - 1].chainLevel != 3)
            {
                //같지 않다면 합칠 필요가 없다.
                if ((blockPanel[usingBlockHeadIndex - 1].targetUnit == blockPanel[usingBlockHeadIndex].targetUnit) && (blockPanel[usingBlockHeadIndex - 1].skillType == blockPanel[usingBlockHeadIndex].skillType))
                {
                    //사용된 블록 앞에 쌓여있는 블록의 헤더 인덱스
                    int frontHeadBlockIndex = blockPanel.IndexOf(blockPanel[usingBlockHeadIndex-1].headBlock);
                    int totalCombineBlockCount = blockPanel[frontHeadBlockIndex].chainLevel+ blockPanel[usingBlockHeadIndex].chainLevel;


                    //내려오는 블록중 마지막 블록 찾기
                    //[0 1 2 3] [4 5]
                    Debug.Log((usingBlockHeadIndex + blockPanel[usingBlockHeadIndex].chainLevel) + "-"+(blockPanel.Count));
                    for (int index = usingBlockHeadIndex + blockPanel[usingBlockHeadIndex].chainLevel; index < blockPanel.Count; index++)
                    {
                        if ((blockPanel[frontHeadBlockIndex].targetUnit != blockPanel[index].targetUnit) || (blockPanel[frontHeadBlockIndex].skillType != blockPanel[index].skillType))
                        {
                            break;
                        }
                        totalCombineBlockCount++;
                    }
                    Debug.Log(frontHeadBlockIndex + " " +totalCombineBlockCount);
                    blockPanel[usingBlockHeadIndex].StartTryCombine(blockPanel.GetRange(frontHeadBlockIndex, totalCombineBlockCount).ToArray());
                }
            }
        }
    }
    
}
