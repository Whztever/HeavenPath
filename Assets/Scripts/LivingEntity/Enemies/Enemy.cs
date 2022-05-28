using UnityEngine;

public class Enemy : LivingEntity
{
    [SerializeField] protected float collisionDamage;
    //防止重复伤害
    private float timeBtwDamage = 0.5f;
    private void Update()
    {
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }

    }
    public override void TakenHit(float damage, Vector3 hitPoint, Vector3 hitDirection){
        TimeController.instance.BulletTime(0.5f,0.5f);
        Debug.Log(333);
       base.TakenHit(damage,hitPoint, hitDirection);
       Debug.Log(444);
       LootSpawner.instance.Spawn(transform.position);

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDead == false)
        {
            if (timeBtwDamage <= 0)
            {
                if (transform.position.x > other.transform.position.x)
                {
                    //怪在右边往左闪避
                    PlayerLife.instance.TakenHit(collisionDamage, other.transform.position, Vector3.left);

                }
                else
                {
                    PlayerLife.instance.TakenHit(collisionDamage, other.transform.position, -Vector3.left);
                }
                timeBtwDamage = 0.5f;
            }


        }
    }
}
