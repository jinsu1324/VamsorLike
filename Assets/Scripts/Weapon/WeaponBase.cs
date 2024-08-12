using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase
{
    // 공격
    public virtual void Attack()
    {
        Debug.Log("공격!");
    }
}
