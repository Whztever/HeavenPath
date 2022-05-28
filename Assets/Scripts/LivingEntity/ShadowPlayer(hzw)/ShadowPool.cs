using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;
    public GameObject shadowPrefab;
    public int shadowCount;

    private Queue<GameObject> avaliableObjects=new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
        //初始化对象池
        FillPool();
        DontDestroyOnLoad(this);
    }

    public void FillPool()
    {
        for(int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

            //取消启用，返回对象池
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        avaliableObjects.Enqueue(gameObject);
    }

    public GameObject GetFromPool()
    {
        Debug.Log("test2");

        if (avaliableObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = avaliableObjects.Dequeue();
        outShadow.SetActive(true);
        Debug.Log("test3");
        return outShadow;
    }

}
