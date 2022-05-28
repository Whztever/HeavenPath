using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRandomBehavior:StateMachineBehaviour
{
    public static float theSkyRandomtimer;
    public float minTime;
    public float maxTime;
   
    private Transform playerPos;
    public float speed;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerPos = PlayerController.instance.transform;
        theSkyRandomtimer = Random.Range(minTime, maxTime);
        Debug.Log("222");
        BossController.instance.StartCoroutine("RandomlyFireCoroutine");
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (theSkyRandomtimer <= 0)
        {
            animator.SetTrigger("skyIdling");
        }
        else {
            theSkyRandomtimer -= Time.deltaTime;
        }

       // Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
       
        //这里动画机有一个不能改变位置的问题，尝试网上种种方法未果，给要动画移动的放在他的子物体试试吧，应该就解决了
      //  animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
       
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
