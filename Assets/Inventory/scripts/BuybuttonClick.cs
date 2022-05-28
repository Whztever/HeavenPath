using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuybuttonClick : MonoBehaviour
{
    public Inventory bag;
    public MyItem sword;
    public MyItem gun;
    public MyItem air;
    public MyItem coffee;
    public Coins Coins;
   public void click()
    {
        int cost = 0;
        var curbtn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        switch (curbtn.name)
        {
            case "buy_sword":
                cost = sword.MyItemPrice;
                //计算费用的方法。
                if ((sword.sold == false)&&(cost<=Coins.Amounts))
                {
                    bag.MyItemlist.Add(sword);
                    InventoryManager.CreatNewMyItem(sword);
                    sword.sold = true;
                    Coins.Amounts = Coins.Amounts - cost;
                }
                break;
            case "buy_gun":
                cost = gun.MyItemPrice;
                if ((gun.sold == false) && (cost <= Coins.Amounts))
                {
                    bag.MyItemlist.Add(gun);
                    InventoryManager.CreatNewMyItem(gun);
                    gun.sold = true;
                }
                break;
            case "buy_airjacket":
                bag.MyItemlist.Add(air);
                break;
            case "coffee":
                bag.MyItemlist.Add(coffee);
                break;
        }
    }
}
