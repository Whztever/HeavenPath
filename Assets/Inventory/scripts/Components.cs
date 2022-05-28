using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Components : MonoBehaviour
{
    public MyItem ComponentsMyItem;
    public Image componentImage;
    public int price;
    public int damage;
    public Text equiped;

    public void MyItemOnClick()
    {
        InventoryManager.updateMyIteminfo(ComponentsMyItem.MyItemInfo);
        
    }
}
