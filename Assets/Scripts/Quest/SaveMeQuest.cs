using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//挂载要被救的自己身上
//做成基类也要写很多脚本，所以不这样
//可以做成奖励列表，每救一个，就给一个功能
public class SaveMeQuest : MonoBehaviour
{
    [Header("救我相关")]
    public bool saved;
    public bool isFinished;//用来让祝福台词只触发一次
    [Header("能量条减少相关")]
    public float energyDelta = 5f;
    // Start is called before the first frame update
    //[Header("奖励给予")]
    
    //这个感觉要设一个GameManager
    //要不要有个物品掉落拾取：todo

    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
