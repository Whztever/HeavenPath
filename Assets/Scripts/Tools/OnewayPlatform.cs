using UnityEngine;

public class OnewayPlatform : MonoBehaviour
{
    //单向平台
    private PlatformEffector2D platformEffect2D;
    [SerializeField] private float holdTime;

    private void Start()
    {
        platformEffect2D = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp(KeyCode.DownArrow))
        {
            holdTime = 0.5f;
        }
        //一直按住
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (holdTime <= 0)
            {
                platformEffect2D.rotationalOffset = 180f;
                holdTime = 0.5f;
            }
            else
            {
                holdTime -= Time.deltaTime;
            }
        }
        
        if (Input.GetButton("Jump"))
        {
            platformEffect2D.rotationalOffset = 0f;
        }
        
    }
}
