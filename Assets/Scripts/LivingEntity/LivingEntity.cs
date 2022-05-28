using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//这是playerController和enemy的基类·（父类）

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float health;//他的级别是外界不能访问但是允许继承自livingentity的子类可以访问
    protected bool isDead;
    [HideInInspector] public bool isHurt;
    public bool isHurtt { get { return isHurt; }  set { isHurt = value; } }

    //阵亡：一个事件
    public event Action onDeath;
    //子类父类的start方法里override
    //通过关键词virtual明确告知，这个start方法我们允许在子类中复写/重写
    //也可以使用base.start方法在父类的基础上添加额外的部分
    public virtual void Start()
    {
        health = maxHealth;
    }

    [ContextMenu("Self Destruct")]//创建一个上下文菜单用来右键自毁
    public virtual void Die()
    {
        isDead = true;
        Destroy(gameObject);
        if (onDeath != null)
            onDeath();
        //onDeath事件：如果是player：回到初始点。取消敌人对player的追踪
        //如果是敌人：判断场上剩余敌人数量，安排下一波；掉落金币
        //如果是Boss：解锁商店老板的场景。
    }

    //敌人收到攻击,粒子效果,可在enemyy里override
    public virtual void TakenHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakenDamage(damage);

    }

    public virtual void TakenDamage(float _damageAmount)
    {
        isHurt =true;
        health -= _damageAmount;
        //Debug.Log("执行");
       StartCoroutine(isAttackCo());
        if (health <= 0 && isDead == false)
        {
            Die();
        }
    }
    public virtual void ChangeSpeed()
    {

    }
    public virtual void RecoverSpeed()
    {

    }
    //解决一次攻击多次受伤问题
      IEnumerator isAttackCo()
    {
        yield return new WaitForSeconds(0.2f);
        isHurt= false;
    }

}