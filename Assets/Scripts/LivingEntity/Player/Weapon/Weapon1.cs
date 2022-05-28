using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : MonoBehaviour
{
    float nextShotTime;
    public float msBetweenShots = 300;//冷却时间0.3s

    private void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("111");
            Attack();
        }
    }
    private void Attack()
    {

        if(!PlayerController.instance.isDashing){
            if (Time.time > nextShotTime)//计时器限制发射频率
            {
                
                PlayerController.instance.isAttack=true;
                nextShotTime = Time.time + (msBetweenShots / 1000);
                transform.GetChild(0).gameObject.SetActive(true);//武器和本体是分开的
            }

        }
    }


}
