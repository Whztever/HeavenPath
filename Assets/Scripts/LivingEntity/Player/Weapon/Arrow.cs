using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float shootSpeed;
    [SerializeField] int minAttack = 3, maxAttack = 10;
    
    int attackDamage;
    [Header("攻击数值可视化")]//可以整到父类里
    [SerializeField]private GameObject damageCanvas;
    //这里可以改
    [SerializeField] private float lifetime;
    public LayerMask collisionMask;
    //最大检测距离
    [SerializeField] private float maxDist=1.0f;
      void Start()
    {
       
        Destroy(gameObject, lifetime);
        
    }
    public void SetSpeed(float newSpeed)
    {
        shootSpeed = newSpeed;
    }
    void Update()
    {
        float moveDistance = shootSpeed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.up * moveDistance,Space.Self);
        
       
    }


    void CheckCollisions(float moveDistance)
    {
       
        //todo 检测platform RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxDist, LayerMask.GetMask("Platform"));
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxDist, LayerMask.GetMask("Enemy"));

        if (hitInfo.collider!=null)
        {
            OnHitObject(hitInfo.collider,transform.position);
        }
    }

    void OnHitObject(Collider2D c, Vector3 hitPoint)
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        attackDamage = (int)Random.Range(minAttack, maxAttack);
        if (damageableObject != null)
        {
            damageableObject.TakenHit(attackDamage, hitPoint,transform.up);
            DamageNum damagable = Instantiate(damageCanvas, hitPoint, Quaternion.identity).GetComponent<DamageNum>();
            damagable.ShowDamage(Mathf.RoundToInt(attackDamage));

        }
        GameObject.Destroy(gameObject);
    }

}