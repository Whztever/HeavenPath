using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager: MonoBehaviour

{
    public static UIManager instance;
    public  Text goldText;
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
        
    }
    private void Update() {
        goldText.text=PlayerItem.instance.gold.ToString();
    }
}
