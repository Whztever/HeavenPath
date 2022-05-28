using UnityEngine;

public class BossLife : Enemy
{
    public static BossLife instance;
    public float diaoyongDamage;
    public HealthBar healthBar;
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
    public override void Start() {
        base.Start();
        //这都治不好你第一次攻击还是无法识别更新血条，先这样吧
        GetComponentInChildren<HealthBar>().UpdateHp();
    }
    public override void TakenHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        diaoyongDamage=damage;
        //这里生成金币奖励
        
        //AudioManager.instance.PlaySound("Impact", transform.position);//打到了
        GetComponentInChildren<HealthBar>().UpdateHp();
        
        if (damage >= health)
        {
           // AudioManager.instance.PlaySound("EnemyDeath", transform.position);
           //Destroy(Instantiate(deatheffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, 2f);
        }
        base.TakenHit(damage, hitPoint, hitDirection);
    }
}