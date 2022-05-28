using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
//这是可行的嫌弃没渐变或许可以加一段动画但是先这样吧
public class CameraController : MonoBehaviour
{

    public GameObject guoduImage;
    Animator anim;
    public float time=1;
    private void Awake() {
        
    }
    void Start()
    {
        anim = guoduImage.GetComponent<Animator>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            anim.enabled=true;
            Invoke("ChangeColor",time);
            anim.SetBool("Guodu", true);
        }
    }
    public void ChangeColor(){
        gameObject.GetComponent<PostProcessVolume>().enabled=false;
    }
}
    

    


