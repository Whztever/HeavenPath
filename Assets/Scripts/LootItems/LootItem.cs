using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootItem : MonoBehaviour
{
    [SerializeField] float minSpeed = 5f;
    [SerializeField] float maxSpeed = 15f;

    //int pickUpStateID = Animator.StringToHash("PickUp");

   // Animator animator;
    protected Text lootMessage;
    [SerializeField]protected float damageIndex; 

    protected virtual void Awake()
    {
       // animator = GetComponent<Animator>();

       

       // lootMessage = GetComponentInChildren<Text>(true);

       // pickUpSFX = defaultPickUpSFX;
    }
    protected virtual  void Start(){
        damageIndex=BossLife.instance.diaoyongDamage;
     }
    protected virtual void Update() {
        StartCoroutine(MoveCoroutine());
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
    
        Debug.Log("!11");
        if(other.tag == "Player")
        {
            Debug.Log("222");
            //PlayerItem.instance.itemAmount += 1;/
            PickUp();
            
            Destroy(gameObject);
        }
       
        
        
    }

//拾取函数
    protected virtual void PickUp()
    {
        
        //canvas启用
       // animator.Play(pickUpStateID);
       // AudioManager.Instance.PlayRandomSFX(pickUpSFX);
    }

//自动飞向玩家的协程--可以设置追踪子弹
    IEnumerator MoveCoroutine()
    {
        float speed = Random.Range(minSpeed, maxSpeed);

        Vector3 direction = Vector3.up;

        while (true)
        {
            if (PlayerLife.instance.gameObject!=null)
            {
                  direction = (PlayerLife.instance.transform.position - transform.position).normalized;
            }

            transform.Translate(direction * speed * Time.deltaTime);

            yield return null;
        }
        
    }
}