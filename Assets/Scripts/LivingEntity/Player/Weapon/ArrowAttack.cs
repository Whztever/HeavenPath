using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAttack : MonoBehaviour
{
    //剑这里可以考虑加个大招射三把箭
    [Header("不同的箭口")]
    public Transform[] arrowSpawn;
    public Arrow arrow;
    public float muzzleVelocity = 35;
    
    [Header("角度移动限制")]
    [SerializeField] private float rotateSpeed = 5f;
    public GameObject theCenterPoint;
    [SerializeField]float rotateMove;
    [SerializeField] float minView = 270f;
    [SerializeField] float maxView = 5f;
    [Header("武器冷却时间")]
    float nextShotTime;
    public float msBetweenShots = 300;//冷却时间0.3s

    private void Update()
    {
        //Debug.DrawLine(arrowSpawn[1].transform.position, arrowSpawn[1].transform.up, Color.green);
       // Debug.Log(transform.localEulerAngles.z);
        
        rotateMove = Input.GetAxisRaw("Fire2");//设置成自己转其实也可以，反正给限制了开火时间
        float rotateMoveTemp=rotateMove;
        if(PlayerController.instance .transform.localScale.x==-1)  rotateMoveTemp=-rotateMove;
        if (transform.localEulerAngles.z <= minView && transform.localEulerAngles.z >= 260 && rotateMoveTemp < 0)
        {
            
        }
        else if (transform.localEulerAngles.z <= maxView && transform.localEulerAngles.z != 0 && rotateMoveTemp > 0)
        {
            
        }
        else
        {
           
            SelfRotation();
        }
        if(Input.GetButtonDown("Fire1")){
             Shoot();//只在攻击时调用射线检测
        }

        
    }
    private void SelfRotation()
    {   
        //第一个参数是中心点，第二个参数是围绕转动的轴
        transform.RotateAround(theCenterPoint.transform.position, Vector3.forward, rotateMove*rotateSpeed * Time.deltaTime);

    }
     private void Shoot()
    {
        //计时器限制发射频率
         if (Time.time > nextShotTime)//计时器限制发射频率
        {

            for (int i = 0; i < arrowSpawn.Length; i++)
            {

                nextShotTime = Time.time + (msBetweenShots / 1000);

                Arrow newArrow= Instantiate(arrow, arrowSpawn[i].position, arrowSpawn[i].rotation) as Arrow;
                // 给他设置速度
                newArrow.SetSpeed(muzzleVelocity);
            }
            //改进一下，射箭这两秒不能移动，且播放动画：
            PlayerController.instance.isShooting=true;
            StartCoroutine("ShootEnd");
            

            //AudioManager.instance.PlaySound(shootAudio, transform.position);
        }

    }
   private IEnumerator ShootEnd(){
       yield return new WaitForSeconds(0.3f);
       PlayerController.instance.isShooting=false;
       

    }
}
