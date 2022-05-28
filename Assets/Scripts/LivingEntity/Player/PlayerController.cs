using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//todolist：crunch、dash、跳跃的一个小效果：在完美跳跃echo里有类似的
public class PlayerController : MonoBehaviour
{
    //单例模式:
    public static PlayerController instance;
    private Rigidbody2D rb;
    private Collider2D coll;
    
    [Header("运动参数")]
    public float currentSpeed, jumpForce;
    private float horizontalMove;
    public Transform groundCheck;
    public LayerMask ground;
    bool jumpPressed;
    int jumpCount;

    [Header("更好的跳跃效果")]
    [SerializeField] private float fallFactor;
    [SerializeField] private float shortJumpFactor;

    [Header("更好的水平加速减速效果")]
    [SerializeField] private float horizontalAcceration=5f;
    [SerializeField] private float horizontalDeceleration=5f;
    [SerializeField] private float maxSpeed=6f;

    [Header("动画效果")]
    private Animator anim;
    public bool isGround, isJump, isDashing;

    [Header("Scene Transition转场")]
    public string scenePassword;
    [Header("对话参数")]
    //先这样写吧，懒得改整洁了
    public bool isTalking = false;
    public bool isShooting =false;
    public bool isCantMoving =false;
    public bool isAttack;
    public float cantMoveTime;

    [Header("冲刺效果")]
    public float DashTime;//冲刺时长
    private float DashTimeLeft;//冲刺剩余时间
    private float LastDash=-10f;//上次冲刺时间点
    public float CDTime;//cool down time
    public float DashSpeed;


    [Header("攻击前进控制")]
    public float AttackForwardSpeed;
    [Header("补偿速度")]
    public float lightSpeed;
    public float heavySpeed;



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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(Time.time >= (LastDash + CDTime))
            {
                //可以执行dash
                readyToDash();

            }
        }
    }

    private void FixedUpdate()
    {
        if (isTalking||isShooting||isCantMoving)
        {
            //这里要改，动画也要是idle当对话时
            rb.velocity=new Vector2(0,0);
            return;
        }
        //加上补偿移动，但是远程攻击就不用，这还要看做不做远程攻击，做就再加参数
        if(isAttack){
           if (Attack1.attackType == "Light")
                rb.velocity = new Vector2(transform.localScale.x * lightSpeed, rb.velocity.y);
            else if (Attack1.attackType == "Heavy")
                rb.velocity = new Vector2(transform.localScale.x * heavySpeed, rb.velocity.y);

        }
        else{
            isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
            
            Debug.DrawLine(groundCheck.position,new Vector3(groundCheck.position.x,groundCheck.position.y-0.1f,0),Color.red);
            if(!isAttack){
                Dash();
                if (isDashing)
                {
                    return;
                }
                GroundMovement();

                Jump();

                SwitchAnim();
            }
        }
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1，0，1
        
        if (horizontalMove != 0)
        {   
            currentSpeed=Mathf.MoveTowards(currentSpeed,maxSpeed,horizontalAcceration*Time.deltaTime);
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
        else
        {
            currentSpeed=Mathf.MoveTowards(currentSpeed,0,horizontalDeceleration*Time.deltaTime);
        }
        //rb.velocity = new Vector2(horizontalMove * currentSpeed, rb.velocity.y);
        rb.velocity = new Vector2(currentSpeed*transform.localScale.x, rb.velocity.y);

    }

    void Jump()//跳跃
    {
        if (isGround)
        {
            jumpCount = 2;//可跳跃数量
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //todo：二段跳可以考虑加个特效Instantiate(jumpEffect, transform.position - Vector3.up, Quaternion.identity);
            jumpCount--;
            jumpPressed = false;
        }
        BetterJump();
    }
    //更好的跳跃效果[重力改变法]
    private void BetterJump()
    {
        if (rb.velocity.y < 0)//角色下落时，速度会越来越快
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallFactor * Time.deltaTime;
        }
        //没有一直按住跳跃
        else if (rb.velocity.y > 0 && !(Input.GetButton("Jump")))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * shortJumpFactor * Time.deltaTime;
        }
    }
    void SwitchAnim()//动画切换
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
        //Debug.Log(isGround);
        //Debug.Log(rb.velocity.y);
        if (isGround)
        {   //Player跳上平台没状态转换应该是因为刚好跳上去falling成false了
            //这样就不去判断底下的条件了没法把jump的值改过来了 
            anim.SetBool("falling", false);
            anim.SetBool("jumping", false);//解决：加上这一句ok
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y <= 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }
    
    public void CantMove(float cantMovetime){
        isCantMoving=true;
        cantMoveTime=cantMovetime;
        StartCoroutine("canMove");
    }
      IEnumerator canMove()
    {
        yield return new WaitForSeconds(cantMoveTime);
        isCantMoving= false;
    }

    void readyToDash()
    {
        isDashing = true;
        DashTimeLeft = DashTime;
        LastDash = Time.time;
         
    }

    void Dash()
    {
        if (isDashing)
        {
            if (DashTimeLeft > 0)
            {
                if (rb.velocity.y > 0 && !isGround)
                {
                    rb.velocity = new Vector2(DashSpeed * transform.localScale.x, jumpForce);//在空中Dash向上
                }
                rb.velocity = new Vector2(DashSpeed * transform.localScale.x, rb.velocity.y);//地面Dash

                DashTimeLeft -= Time.deltaTime;
               
               // ShadowPool.instance.GetFromPool();
            }
            if (DashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
        
        
    }
    //不放在最后一帧，因为连击存在预输入
    public void AttackOver()
    {   anim.SetBool("HeavyAttack", false);
        anim.SetBool("LightAttack", false);
        PlayerController.instance.isAttack = false;
    }
}