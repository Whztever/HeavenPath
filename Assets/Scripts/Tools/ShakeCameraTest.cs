using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//配置虚拟摄像机后不可以改变相机的transform 这是无用的
public class ShakeCameraTest: MonoBehaviour
{
    public float strength;
    public float duration;
    public Transform camera;
    void Start(){
        CameraShake(duration,strength);
    }
    public void CameraShake(float duration, float strength)
    {
        StartCoroutine(Shake(duration, strength));
    }

    IEnumerator Shake(float duration, float strength)
    {
        
       
        Vector3 startPosition = camera.position;

        while (duration > 0)
        {
            //单位球儿里的随机
            camera.position = Random.insideUnitSphere * strength + startPosition;
            duration -= Time.deltaTime;
            yield return null;
        }
        camera.position = startPosition;
    }
}