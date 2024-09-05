using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPObject : MonoBehaviour
{
    // 경험치 획득했을 때 액션
    public static event Action OnGetExp;


    // 경험치에 닿았을때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            OnGetExp();
            Destroy(this.gameObject);
        }
    }
}
