using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAchivement : MonoBehaviour
{
    private int TotalDamage;

    void Start()
    {
        TestPlayer.AttackEvent += AddTotalDamage;
    }

    public bool AddTotalDamage(AttackArgs attackArgs)
    {
        TotalDamage += attackArgs.Damage;

        Debug.Log($"TotalDamage : {TotalDamage}");

        return false;
    }
}
