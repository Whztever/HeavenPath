using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDamageable
{
    bool isHurtt { get; set; }
    void TakenDamage(float _damageAmount);
    //z这里是不需要在接口去写方法体的，因为这个方法本身就是等待实现的脚本去实现的，
    //任何使用这个接口的脚本都必须在他自己中实现这个方法
    void TakenHit(float damage, Vector3 hitPoint, Vector3 hitDirection);
}