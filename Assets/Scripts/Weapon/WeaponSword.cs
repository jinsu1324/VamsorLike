using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : WeaponBase
{
    public override void Attack()
    {
        base.Attack();
        Debug.Log("�߰� �� ������ ������!");
    }
}
