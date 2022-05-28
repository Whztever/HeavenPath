using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    public static BossController instance;
    private Animator anim;

    [Header("移动")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float moveRotationAngle = 25f;

    [Header("开火")]
    [SerializeField] private GameObject[] projectiles;
    //[SerializeField] protected Transform muzzle;
    //[SerializeField] protected ParticleSystem muzzleVFX;
    [SerializeField] private float minFireInterval;//开火间隔
    [SerializeField] private float maxFireInterval;
    [SerializeField] private Transform muzzle;//枪口
    [SerializeField] private float continuousFireDuration = SkyRandomBehavior.theSkyRandomtimer-1;//一轮开枪持续时间

    float paddingX;
    float paddingY;

    private Vector3 targetPosition;

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    WaitForSeconds waitForFireInterval;

    float health;
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
        waitForFireInterval = new WaitForSeconds(minFireInterval);

    }


    private void Start()
    {
        anim = GetComponent<Animator>();
        BossLife.instance.onDeath += BossDeath;
        health = BossLife.instance.health;

    }

    private void Update()
    {
        if (health <= 25)
        {
            anim.SetTrigger("stageTwo");
        }




    }
    private void BossDeath()
    {
        anim.SetTrigger("death");
    }
    //在这边也写一些需要调用协程的函数吧

    /*

        protected virtual void Awake()
        {
            var size = transform.GetChild(0).GetComponent<Renderer>().bounds.size;
            paddingX = size.x / 2f;
            paddingY = size.y / 2f;
        }

        protected virtual void OnEnable()
        {
            StartCoroutine(nameof(RandomlyMovingCoroutine));
            StartCoroutine(nameof(RandomlyFireCoroutine));
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }

        IEnumerator RandomlyMovingCoroutine()
        {
            transform.position = Viewport.Instance.RandomEnemySpawnPosition(paddingX, paddingY);

            targetPosition = Viewport.Instance.RandomRightHalfPosition(paddingX, paddingY);

            while (gameObject.activeSelf)
            {
                if (Vector3.Distance(transform.position, targetPosition) >= moveSpeed * Time.fixedDeltaTime)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
                    transform.rotation = Quaternion.AngleAxis((targetPosition - transform.position).normalized.y * moveRotationAngle, Vector3.right);
                }
                else
                {
                    targetPosition = Viewport.Instance.RandomRightHalfPosition(paddingX, paddingY);
                }

                yield return waitForFixedUpdate;
            }
        }*/
    
    IEnumerator RandomlyFireCoroutine()
    {

        
        Debug.Log("111");
        float continuousFireTimer = 0f;
        while (continuousFireTimer < continuousFireDuration)
        {
            foreach (var projectile in projectiles)
            {
                Instantiate(projectile, muzzle.position, Quaternion.identity);

            }
            continuousFireTimer += minFireInterval;
            // AudioManager.Instance.

            yield return waitForFireInterval;
        }
        yield return new WaitForSeconds(Random.Range(minFireInterval, maxFireInterval));

        //AudioManager.Instance.
        // muzzleVFX.Play();
    }
}
//}

//}
