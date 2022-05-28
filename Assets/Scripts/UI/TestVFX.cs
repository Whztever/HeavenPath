using UnityEngine;

public class TestVFX : MonoBehaviour
{
    [SerializeField] GameObject DarkMagicVFX;
    void Start(){

        Instantiate(DarkMagicVFX,transform.position,Quaternion.identity);
        
    }
}
