using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{

    public Image clockFrontground;
    public Image clockBackground;
    public GameObject currentTime;

    [Range(0, 60)]
    public int gameTime;//游戏需要的时长，后期可以改变1是一分钟为单位

    private void Awake()
    {
        //clockBackground.fillAmount = (float)gameTime / 60;
        clockFrontground.fillAmount = (float)gameTime / 60;
    }

    private void Update()
    {
        float currentTimeAngle = (float)((Time.realtimeSinceStartup * 360) / 60);
        currentTime.transform.eulerAngles = new Vector3(0, 0, -currentTimeAngle);
        clockFrontground.fillAmount = (float)Time.realtimeSinceStartup / 60;
        //Debug.Log(currentTimeAngle);

        Alert();
        CheckGameOver();
    }

    private void Alert()
    {
        if(clockFrontground.fillAmount >=0.9)
        {
            //currentTime.GetComponent<Image>().color = new Vector4(1, 0, 0, 1);//RGBA
            clockBackground.color = new Vector4(1, 0, 0, 1);
        }
    }

    private void CheckGameOver()
    {
        if(Time.realtimeSinceStartup >= gameTime * 60)
        {
            
            SceneManager.LoadSceneAsync("NormalRoom");
        }
        
    }

}