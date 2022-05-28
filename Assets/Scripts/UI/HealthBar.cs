using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//开放调用UpdateHp()
public class HealthBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;

    [SerializeField] public float hp;
    [SerializeField] private float maxHp;
    [SerializeField] private float hurtSpeed = 0.005f;
    LivingEntity livingEntityObject;

    private void Awake()
    {
        
        livingEntityObject = GetComponentInParent<LivingEntity>();
        maxHp = livingEntityObject.maxHealth;
       // 我充满了不理解当前health不是0为什么这里永远显示0？
       
     
    }
    private void Start()
    {
        //hp = maxHp;
         hp= livingEntityObject.health;
    }
   
    //选择用受伤时协程来减少性能消耗
    public void UpdateHp()
    {
        StartCoroutine(UpdateHpCo());
    }

    IEnumerator UpdateHpCo()
    {
        Debug.Log(hp);
        //percentText.text = targetFillAmount.ToString("F2");
        hp = livingEntityObject.health;
        hpImage.fillAmount = hp / maxHp;
        while (hpEffectImage.fillAmount >= hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
            yield return new WaitForSeconds(0.005f);
           
        }
        if (hpEffectImage.fillAmount < hpImage.fillAmount)
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }

}
