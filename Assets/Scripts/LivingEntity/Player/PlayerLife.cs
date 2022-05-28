using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife :LivingEntity
{
    public static PlayerLife instance;
   
    
    [Header("击退效果")]
    [SerializeField]float deltaPostionX=3f;
    [SerializeField]float backSpeed=3f;//被击退的速度
    [SerializeField]float cantMoveTime=3f;//被击退的速度
    float hitTempDirection;
    bool meetWall;
    bool meetWall2;
    [Header("防止反弹出画面检测")]
    public Transform wallCheck;
    public Transform wallCheck2;
    public LayerMask wall;

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
        DontDestroyOnLoad(gameObject);
        
    }
    public override void Start() {
        base.Start();
        GetComponentInChildren<HealthBar>().UpdateHp();
    }

    public override void TakenHit(float _damageAmount,Vector3 hitPoint, Vector3 hitDirection)
    {
           //todo播放受伤动画，改变动画参数
            //todo播放受伤声音
            //todo实现无敌帧
            //实现击退效果ok
            //实现僵直,这个在攻击、打开对话面板时都用过的ok
            //马上还要用在受到攻击、跳跃太高后的下落
            //现在我把这个僵直改成一个PlayerController里的函数吧
            
        hitTempDirection=hitDirection.x;
        GetComponentInChildren<HealthBar>().UpdateHp();
        if (_damageAmount >= health)
        {
           // AudioManager.instance.PlaySound("EnemyDeath", transform.position);
           //Destroy(Instantiate(deatheffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, 2f);
        }
        base.TakenDamage(_damageAmount);
        StartCoroutine("HitBack");
    }
    IEnumerator HitBack(){
        Vector3 originalPosition = transform.position;
        Vector3 dirPosition=transform.position+new Vector3(deltaPostionX*hitTempDirection,0,0);
      //  playerSkinMaterial.color = Color.red;可以考虑颜色变化
        float percent = 0;
        while (percent <= 1)
        {
            //这里要加上一个墙体检测，这样才不会调出墙外
            meetWall=Physics2D.OverlapCircle(wallCheck.position,0.1f,wall);
            meetWall2=Physics2D.OverlapCircle(wallCheck2.position,0.1f,wall);
            if(meetWall||meetWall2) yield break;
            percent += Time.deltaTime * backSpeed;
            float interpolation = percent * percent;
            transform.position = Vector3.Lerp(originalPosition, dirPosition, interpolation);
            yield return null;//保證每次循环遍历都从上一次停止的地方开始执行
        }
       // PlayerController.instance.CantMove(cantMoveTime);在空中太怪了
        yield return null;
    }
    
}
