using UnityEngine;
using UnityEngine.UI;

//这个脚本将会放在【UI Canvas】游戏对象上 任务UI列表
public class QuestManager : MonoBehaviour
{   
    public static QuestManager instance;

    public GameObject[] questArray;

    public GameObject questPanel;//打开关闭的控制

    public Text expText, goldText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateQuestList();
        questPanel.SetActive(false);
    }

    //这个方法将会在【领取好任务】【任务完成后】调用
    public void UpdateQuestList()
    {
        //如果我们要将【完成的任务】移出UI任务列表，我们就不能这么遍历，而是遍历有多少UI任务栏 TODO
        for(int i = 0; i < PlayerItem.instance.questList.Count; i++)//有多少个任务显示多少个List，而不是有多少List显示多少个任务
        {
            questArray[i].transform.GetChild(0).GetComponent<Text>().text = PlayerItem.instance.questList[i].questName;

            if(PlayerItem.instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
            {
                questArray[i].transform.GetChild(1).GetComponent<Text>().text
                = "<color=red>" + PlayerItem.instance.questList[i].questStatus + "</color>";
            }
            else if (PlayerItem.instance.questList[i].questStatus == Quest.QuestStatus.Completed)
            {
                questArray[i].transform.GetChild(1).GetComponent<Text>().text
                = "<color=lime>" + PlayerItem.instance.questList[i].questStatus + "</color>";
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerController.instance.isTalking == false)
        {
            questPanel.gameObject.SetActive(!questPanel.activeInHierarchy);
        }

        //SOLVED 修复：当开启【UI任务列表】时，和NPC开启对话【UI任务列表】还开启的问题
        //改成打开面板不能走动或者打开面板后速度走路下降都可以
        if (PlayerController.instance.isTalking && questPanel.activeInHierarchy)
        {
            questPanel.gameObject.SetActive(false);
        }
    }

    public void UpdateUIText()
    {
        expText.text = "EXP: " + PlayerItem.instance.exp;
        goldText.text = "GOLD: " + PlayerItem.instance.gold;
    }
}
