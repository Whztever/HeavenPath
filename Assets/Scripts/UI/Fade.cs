using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    //留着放在UI里吧，懒得写了
    private Animator Anim;
    private void Start() {
         Anim = GetComponent<Animator>();
    }
    public  void FadePlay(float speed){
        
        Anim.enabled=true;

    }
}
