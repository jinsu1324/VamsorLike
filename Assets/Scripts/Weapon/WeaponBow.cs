using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBow : WeaponBase
{
    public override void Attack()
    {
        base.Attack();
        Debug.Log("�߰� Ȱ ġ����� ������!");
    }
}
