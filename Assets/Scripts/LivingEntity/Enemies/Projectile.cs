using UnityEngine;
//和箭重复了或许可以考虑继承一下
public class Projectile : MonoBehaviour
{
    //[SerializeField] GameObject hitVFX;
    [SerializeField] float attackDamage=3f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float lifetime=1f;

    //protected GameObject target;
    void Awake()
    {
        if (moveDirection != Vector2.down)
        {
            transform.rotation = Quaternion.FromToRotation(Vector2.down, moveDirection);
        }

    }
    private void Start() {
         Destroy(gameObject, lifetime);
    }

    private void Update() {
        
        Move();

            
    }
   

    //另一种检测方式属于是
    public void OnCollisionEnter2D(Collision2D collision)
    {
       ;//可以拼刀的设计
       // if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable iDamageable))
       if(collision.gameObject.GetComponent<PlayerLife>())
        {
           
            //为什么这里调用不到healthbar，简直是宇宙未解之谜：懂了，原因不在这而在于第一次调用
            
            PlayerLife.instance.TakenHit(attackDamage,transform.position,transform.up);
            
            Destroy(gameObject);
            
        }
    }

    
   // protected void SetTarget(GameObject target) => this.target = target;

    public void Move() => transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

}