using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBow : WeaponBase
{
    public override void Attack()
    {
        base.Attack();
        Debug.Log("추가 활 치명상을 입혔다!");
    }
}
