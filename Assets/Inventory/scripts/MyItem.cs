using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New MyItem",menuName ="Inventory/New MyItem")]
public class MyItem : ScriptableObject
{
    public string MyItemName;
    public Sprite MyItemImage;
    public int MyItemDamage;
    public string MyItemInfo;
    public bool equiped;
    public bool sold;
    public int MyItemPrice;
}
