using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct AttackArgs
{
    public int Damage;
    public int Val;
}

public class TestPlayer : MonoBehaviour
{
    public delegate bool AttackDelegate(AttackArgs attackArgs);
    public static event AttackDelegate AttackEvent;

    private int _hp = 100;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackArgs attackArgs = new AttackArgs() { Damage = 10 };
            Hit(attackArgs);

            AttackEvent?.Invoke(attackArgs);
        }
    }

    public bool Hit(AttackArgs attackArgs)
    {
        _hp -= attackArgs.Damage;
        Debug.Log(_hp);

        if (_hp <= 0)
        {
            Debug.Log("Die");
            return true;            
        }

        return false;
    }

    public bool DamageEft(AttackArgs attackArgs)
    {
        return false;
    }

}
