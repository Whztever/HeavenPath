using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//开放调用UpdateEp()
public class EnergyBar : MonoBehaviour
{
    public static EnergyBar instance;

    public Image epImage;
    public Image epEffectImage;

    [SerializeField] public float ep;
    [SerializeField] private float maxEp;
    [SerializeField] private float hurtSpeed = 0.005f;
    

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
        maxEp = PlayerEnergy.instance.maxEp;
        ep = PlayerEnergy.instance.ep;
    }

    //选择用受伤时协程来减少性能消耗
    public  void UpdateEp()
    {
        StartCoroutine(UpdateEpCo());
    }

    IEnumerator UpdateEpCo()
    {
        Debug.Log(ep);
        //percentText.text = targetFillAmount.ToString("F2");
        ep = PlayerEnergy.instance.ep;
        epImage.fillAmount = ep / maxEp;
        while (epEffectImage.fillAmount >= epImage.fillAmount)
        {
            epEffectImage.fillAmount -= hurtSpeed;
            yield return new WaitForSeconds(0.005f);

        }
        if (epEffectImage.fillAmount < epImage.fillAmount)
        {
            epEffectImage.fillAmount = epImage.fillAmount;
        }
    }

}
