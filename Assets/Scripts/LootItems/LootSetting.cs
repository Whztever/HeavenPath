using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//类
[System.Serializable] public class LootSetting
{
    public GameObject prefab;
    [Range(0f, 100f)] public float dropPercentage;

//当被攻击到时，就会调用这个来生成战利zhege 写在spawner里里，回来可以改一下
   /* public void Spawn(Vector3 position)
    {
        if (Random.Range(0f, 100f) <= dropPercentage)
        {
           // GameObject Gold=Instantiate(prefab,position);
            
        }
    }*/
}