using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    // 解决攻击时间间隔问题
    [SerializeField] int minAttack = 3, maxAttack = 10;
    
    int attackDamage;
    [Header("攻击数值可视化")]//可以整到父类里
    [SerializeField]private GameObject damageCanvas;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            attackDamage = (int)Random.Range(minAttack, maxAttack);
            IDamageable enemyDamage = other.GetComponent<IDamageable>();
            if (!enemyDamage.isHurtt)
            {
                enemyDamage.TakenHit(attackDamage, other.transform.position, new Vector3((transform.localScale.x), 0, 0));
                // todo 镜头摇晃FindObjectOfType<CameraController>().CameraShake(0.4f);
                //Instantiate(hitEffect, hitTrans.position, Quaternion.identity);

                //显示伤害数字
                 DamageNum damagable = Instantiate(damageCanvas, other.transform.position, Quaternion.identity).GetComponent<DamageNum>();
                damagable.ShowDamage(Mathf.RoundToInt(attackDamage));

                //todo KnockBack Effect 击退效果 反方向移动，从角色中心点「当前位置」向敌人位置方向「目标点」移动
                Vector2 difference = other.transform.position - transform.position;
                difference.Normalize();
                other.transform.position = new Vector2(other.transform.position.x + difference.x / 2,
                                                       other.transform.position.y);
            }
        }
        //todo/If Our Sword cut hit wall「如果我们的刀砍到了墙壁，则产生一个特效并且震动」
        /* if(other.gameObject.tag == "Wall")
         {
             FindObjectOfType<CameraController>().CameraShake(0.5f);
             Instantiate(hitEffect, hitTrans.position, Quaternion.identity);
         }*/
    }
    //在刀光动画结束后的事件调用
    public void EndAttack()
    {
        gameObject.SetActive(false);
        Weapon1.isAttack = false;
    }
}
