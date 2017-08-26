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
        //dropTable.Add(0);
        //dropTable.Add(1);
        //dropTable.Add(0);
        //dropTable.Add(0);

        //dropTable.Add(1);
        //dropTable.Add(1);
        //dropTable.Add(1);
        //dropTable.Add(1);
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
            //블록이 가득차지 않았다면 [블록갯수 8이하].
            if (lastBlockNextIndex < 8)
            {
                //주기를 검사한다. [드랍을 위해서]
                if (currentGeneratingPeriod < blockGeneratePeriod)
                {
                    currentGeneratingPeriod += Time.deltaTime;
                }
                else
                {
                    //쿨탐 초기화하고
                    currentGeneratingPeriod = 0;

                    //이건 드랍할 유닛 임시 설정. 그냥 랜덤임 나중에 지워야할 테스트 코드
                    Unit randomUnit;
                    if (Random.value >=0.5f/*dropTable[lastBlockNextIndex]==0*/)
                        randomUnit= GameManager.Instance.PlayerUnitList[0];
                    else
                        randomUnit= GameManager.instance.PlayerUnitList[1];

                    //임시 블록을 꺼낸 다음 초기화후 드랍시킨다.
                    Block tempBlock = Pop();
                    tempBlock.SetBlockData(1, randomUnit, E_SkillType.Normal);
                    tempBlock.DropDownTargetTransform = blockPanelPostionList[lastBlockNextIndex];
                    blockPanel.Add(tempBlock);
                    tempBlock.StartDropDown();
                    tempBlock.StartDropCombine();

                    // 문제점은 블록이 떨어져서 재정렬 해야할 배열이 중간에 바뀌면 이게 문제를 일으킨다.
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
        int currentChainCount = 1;
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
        
            }
            else
            {
                blocks[index].chainLevel = remainBlocksChainLevel;

                if(remainBlocksChainLevel == 2)
                {

                }

            }

            //헤더블록 재설정
            if (currentChainCount == 3)
            {
                headIndex += 3;
                currentChainCount = 1;
                remainBlocks -= 3;
            }
            else
            {
                currentChainCount++;

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
        int usedBlockHeadIndex = blockPanel.IndexOf(headBlock);//사용된 블록의 헤더블록의 인덱스
        int usingChain = block.chainLevel;              //사용된 블록의 체인수

        block.targetUnit.skillQueue.AddAction(block.targetUnit.skillList[usingChain-1]);
        /*
         * headBlock.TargetUnit.SkillUse(N체인);
         */
        //블록 풀에 사용한 블록만큼 반환한다.
        //헤더 블록을 기준으로 체인갯수만큼 반환
        foreach (Block usingBlock in blockPanel.GetRange(usedBlockHeadIndex,usingChain))
        {
            Push(usingBlock);
        }
        blockPanel.RemoveRange(usedBlockHeadIndex, usingChain);

        //드랍이 가능한 마지막 인덱스를 체인값만큼 빼서 재설정.
        lastBlockNextIndex -= usingChain;
        //당겨진 블록 위치부터 


        


        //합쳐질 블록 계산
        if(lastBlockNextIndex > usedBlockHeadIndex)
        {
            if (usedBlockHeadIndex != 0)
            {
                //사용된 블록의 앞에 체인의 합이 3이면 합칠 필요가 없다.
                if (blockPanel[usedBlockHeadIndex - 1].chainLevel != 3)
                {
                    //같지 않다면 합칠 필요가 없다.
                    if ((blockPanel[usedBlockHeadIndex - 1].targetUnit == blockPanel[usedBlockHeadIndex].targetUnit) && (blockPanel[usedBlockHeadIndex - 1].skillType == blockPanel[usedBlockHeadIndex].skillType))
                    {
                        //사용된 블록 앞에 쌓여있는 블록의 헤더 인덱스
                        int frontHeadBlockIndex = blockPanel.IndexOf(blockPanel[usedBlockHeadIndex - 1].headBlock);
                        int totalCombineBlockCount = blockPanel[frontHeadBlockIndex].chainLevel + blockPanel[usedBlockHeadIndex].chainLevel; //1 + 3 = 4 


                        //내려오는 블록중 마지막 블록 찾기
                        //
                        //Debug.Log((usingBlockHeadIndex + blockPanel[usingBlockHeadIndex].chainLevel) + "-"+(blockPanel.Count));

                        for (int index = usedBlockHeadIndex + blockPanel[usedBlockHeadIndex].chainLevel; index < blockPanel.Count; index++)
                        {
                            //후열이 드랍중이 아니여야 합칠 수 있음.
                            if (!blockPanel[index].canUse)
                            {
                                break;
                            }
                            else
                            {
                                //
                                if ((blockPanel[usedBlockHeadIndex].targetUnit == blockPanel[index].targetUnit) && (blockPanel[usedBlockHeadIndex].skillType == blockPanel[index].skillType))
                                {
                                    totalCombineBlockCount++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        //Debug.Log(frontHeadBlockIndex + " " +totalCombineBlockCount);
                        blockPanel[usedBlockHeadIndex].StartTryCombine(blockPanel.GetRange(frontHeadBlockIndex, totalCombineBlockCount).ToArray());
                    }
                }
            }
        }
        //드롭다운 시킨다.
        int dropDownBlockCount = blockPanel.Count - usedBlockHeadIndex;
        for (int index = usedBlockHeadIndex; index < usedBlockHeadIndex + dropDownBlockCount; index++)
        {
            blockPanel[index].DropDownTargetTransform = blockPanelPostionList[index];
            blockPanel[index].StartDropDown();
        }


    }
    
}
