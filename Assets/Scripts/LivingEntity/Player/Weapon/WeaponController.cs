using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController instance;
   // public Transform weaponHold;
    public Transform[] allWeapons;
    int weaponIndex;
    int weaponAmount;
    public Transform currentWeapon;

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
        currentWeapon=transform.GetChild(0);
        weaponAmount=allWeapons.Length;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(currentWeapon!=null){
                Destroy(currentWeapon.gameObject);
                weaponIndex++;
                weaponIndex%=weaponAmount;
                //音效：这里加个切换枪
                currentWeapon=Instantiate(allWeapons[weaponIndex],transform.position,transform.rotation);
                currentWeapon.transform.parent=this.transform;
                   }
        }
    }
}
