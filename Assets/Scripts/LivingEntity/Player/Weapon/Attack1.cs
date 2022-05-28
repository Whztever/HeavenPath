using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    [Header("傷害值")]
    [SerializeField] int minAttack = 3, maxAttack = 10;
    int attackDamage;
    [Header("攻击数值可视化")]//可以整到父类里
    [SerializeField]private GameObject damageCanvas;
    [Header("连击时停")]
    public int lightPause;
    public int heavyPause;
    [Header("连击判断")]
    public float interval = 2f;
    [SerializeField]private float timer;
    private bool isAttack;
    public static string attackType;
    [SerializeField]private int comboStep;
    [Header("冷却时间")]
     public float msBetweenShots = 300;
    float nextShotTime;
    //冷却时间0.3s

    public Animator anim;





    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetButtonDown("Fire1")&&!PlayerController.instance.isAttack)
        {
            if(!PlayerController.instance.isDashing)
                if (Time.time > nextShotTime)//计时器限制发射频率
                {
                    PlayerController.instance.isAttack=true;
                    nextShotTime = Time.time + (msBetweenShots / 1000);
                    Attack();
                }
        }
        //debug成功这里当然要放在update要不然.deltaTime是调用帧时间
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                comboStep = 0;
            }
        }
    }

    //为什么运行到lightattack2会卡住--一个可以自己重播的功能
    void Attack()
    {
        comboStep++;
        attackType = "Light";
        
        if (comboStep > 2 && timer>0){
            comboStep = 0;
            attackType = "Heavy";
            anim.SetBool("HeavyAttack", true);
            timer = interval;
        }
        else{
            attackType = "Light";
            anim.SetBool("LightAttack",true);
            anim.SetInteger("ComboStep", comboStep);
            timer = interval;
        }
        
       
    }
    /*这段放到controller里
    //不放在最后一帧，因为连击存在预输入
    public void AttackOver()
    {
        PlayerController.instance.isAttack = false;
    }
*/


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            attackDamage = (int)Random.Range(minAttack, maxAttack);

            Debug.Log("We have Hitted the Enemy!");
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
        
    }

   // public void EndAttack()
  //  {
      //  gameObject.SetActive(false);
    //    Weapon1.isAttack = false;
    //}

}
