using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueBox;//显示或隐藏panel
    public Text dialogueText, nameText;
    //保证句子不显示在一行
    [TextArea(1, 3)] public string[] dialogueLines;
    [SerializeField] private int currentLine;

    private bool isScrolling;//禁止没有滚完玩家就跳对话
    [SerializeField] private float scrollingSpeed;

    public Talkable talkable;//采用获取脚本的方法访问变量
    //这些参数应该从我的一个新的类穿过来，而且奖励和talkable里生成新的对话也要有一个新的类
    //这个放在talkable里
    //[Header("救我相关")]
    //怎么尝试get
    //public SaveMeQuest saveMeQuest;
    //设计成单例模式挂在Dialoguepanel下

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    //打开对话窗口，文字滚动
    public void ShowDialogue(string[] _newLines, bool _hasName)
    {
        dialogueLines = _newLines;
        currentLine = 0;

        CheckName();
        SetTextAlign(_hasName);

        StartCoroutine(ScrollingText());

        dialogueBox.SetActive(true);
        //因为单例模式如果没名字会沿用上一次调用的名字
        nameText.gameObject.SetActive(_hasName);

        PlayerController.instance.isTalking = true;
    }
    private void Update()
    {
        if(dialogueBox.activeInHierarchy)//只在激活panel时检测按下左键
        {
            //当任务为save类
            if (talkable.saveMeQuest&&Input.GetKeyUp(KeyCode.Return) &&!talkable.saveMeQuest.saved)
            {
                PlayerEnergy.instance.ep -= talkable.saveMeQuest.energyDelta;
                EnergyBar.instance.UpdateEp();
                //任务完成
                //todo如果save以后1，要调用精力条，精力--2对话结束后要开出奖励list
                talkable.saveMeQuest.saved = true;
                Debug.Log("任务完成");
                if (!talkable.saveMeQuest.isFinished)
                {
                    ShowDialogue(talkable.congratsLines, talkable.hasName);//祝福的台词
                    talkable.saveMeQuest.isFinished = true;//开关，保证一次
                    //对话结束后给奖励--要改
                    //talkable.questable.OfferRewards();
                }
                

            }
            //按下并松开右shift键
            if (Input.GetKeyUp(KeyCode.RightShift)  && !isScrolling)
            {
                currentLine++;

                if (currentLine < dialogueLines.Length)
                {
                    CheckName();
                    StartCoroutine(ScrollingText());//Letter by Letter Show
                }
                #region
                else// 对话即将结束时候
                {
                    if ((GetQuestResult()) && talkable.questable.isFinished == false)//如果当前对话的这个任务【已经完成】
                    {
                        ShowDialogue(talkable.congratsLines, talkable.hasName);//祝福的台词
                        talkable.questable.isFinished = true;//开关，保证一次
                        print(string.Format("QUEST: {0} HAS COMPLETED", talkable.questable.quest.questName));
                        //对话结束后给奖励
                        talkable.questable.OfferRewards();

                        //补充：任务完成以后可以将原先的任务，从questList中移除，不移除也可以，根据游戏要求决定
                        //for(int i = 0; i < Player.instance.questList.Count; i++)
                        //{
                        //    if(Player.instance.questList[i].questName == talkable.questable.quest.questName)
                        //    {
                        //        Player.instance.questList.RemoveAt(i);
                        //    }
                        //调用更新ui列表
                        //}
                    }
                    else// 如果当前对话的这个任务【没有完成】
                    {
                        //当对话结束
                        PlayerController.instance.isTalking = false;
                        dialogueBox.SetActive(false);

                        if (talkable.questable == null)
                        {
                            Debug.Log("There is no Quest on this person");
                        }
                        else
                        {
                            talkable.questable.DelegateQuest();

                            //这里其实是针对【收集类类型】的任务，如果在DelegateQuest方法调用以后
                            //我们直接判断我们是否已经完成了这个任务
                            //这部分先转移到DelegateQuest方法中试试
                            //if (GetQuestResult() && talkable.questable.isFinished == false)
                            //{
                            //    ShowDialogue(talkable.congratsLines, talkable.hasName);
                            //    talkable.questable.isFinished = true;
                            //}
                        }

                        //这部分是当我们和任务要求的游戏对象，比如隐藏NPC对话时，hasTalked等于True
                        if (talkable.questTarget != null)
                        {
                            for (int i = 0; i < PlayerItem.instance.questList.Count; i++)
                            {
                                if (talkable.questTarget.questName == PlayerItem.instance.questList[i].questName)
                                {
                                    talkable.questTarget.hasTalked = true;
                                    talkable.questTarget.CheckQuestIsComplete();
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                #endregion
            }
        }
    }

    //当和委派任务的NPC完成对话后调用，检查任务是否已经完成，如果完成的话返回值为True
    public bool GetQuestResult()
    {
        if (talkable.questable == null)
            return false;

        for (int i = 0; i < PlayerItem.instance.questList.Count; i++)
        {
            if (talkable.questable.quest.questName == PlayerItem.instance.questList[i].questName
                && PlayerItem.instance.questList[i].questStatus == Quest.QuestStatus.Completed)
            {
                talkable.questable.quest.questStatus = Quest.QuestStatus.Completed;
                return true;
            }
        }

        return false;
    }

    // 可对话的NPC对话【居左对齐】工具类游戏对象，比如路标【居中对齐】
    private void SetTextAlign(bool _hasName)
    {
        if (_hasName)
            dialogueText.alignment = (UnityEngine.TextAnchor)TextAlignment.Left;
        else
            dialogueText.alignment = (UnityEngine.TextAnchor)TextAlignment.Center;
    }

    //检查对话内容是否含有对话者的名字
    private void CheckName()
    {
        if(dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");//在NameText处显示名字，并且去除标记n-
            currentLine++;//跳过显示名字的这一行
        }
    }

    //字母滚动效果
    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueText.text = "";//清空

        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;//HELLO => H->E->L->L->O//MARKER Letter by Letter Show
            yield return new WaitForSeconds(scrollingSpeed);
        }

        isScrolling = false;
    }

}
