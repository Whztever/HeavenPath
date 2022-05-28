using UnityEngine;
//继承的加血的
public class ScoreBonusPickUp : LootItem
{
    [SerializeField] int scoreBonus=100;

    //调用伤害
     protected override void Start(){
         base.Start();
         scoreBonus-=(int)damageIndex;
     }
     protected override void Update(){
         base.Update();
     }
    protected override void PickUp()
    {
       
       //canvas启用

        PlayerItem.instance.gold += scoreBonus;
        base.PickUp();
    }
}