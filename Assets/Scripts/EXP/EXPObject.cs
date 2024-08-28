using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPObject : MonoBehaviour
{
    // EXP 획득했을때 처리될 함수들 액션
    public static event Action<EXP> OnGetEXP;

    // 경험치에 닿았을때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {          
            OnGetEXP(EXPManager.Instance.HeroExp);
            Destroy(this.gameObject);
        }
    }
}
