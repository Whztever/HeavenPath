using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerItem : MonoBehaviour
{
    public static PlayerItem instance;


    public int exp, gold, MyItemAmount;

    //之后其他脚本就可以获得这个list了
    public List<Quest> questList = new List<Quest>();

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
}
