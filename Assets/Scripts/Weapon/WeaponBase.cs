using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : SerializedMonoBehaviour
{
    // ����
    public virtual void Attack()
    {
        Debug.Log("����!");
    }
}
