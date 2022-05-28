using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory Mybag;
    public GameObject componentGrid;
    public Components componentPrefab;
    public Text componentInfo;
    public MyItem swordset;
    void Awake()
    {
        if (instance != null)
            Destroy(this);
            instance = this;
        swordset.sold = false;
    }


    public static void updateMyIteminfo(string info)
    {
        instance.componentInfo.text = info;
    }

    public static void CreatNewMyItem(MyItem MyItem)
    {
        Components newMyItem = Instantiate(instance.componentPrefab, instance.componentGrid.transform.position, Quaternion.identity);
        newMyItem.gameObject.transform.SetParent(instance.componentGrid.transform);
        newMyItem.gameObject.transform.localScale = new Vector3(1, 1, 1);
        newMyItem.ComponentsMyItem = MyItem;
        newMyItem.componentImage.sprite = MyItem.MyItemImage;
        newMyItem.price = MyItem.MyItemPrice;
        newMyItem.damage = MyItem.MyItemDamage;
    } 
}
