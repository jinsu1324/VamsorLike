using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPObject : MonoBehaviour
{
    // EXP ȹ�������� ó���� �Լ��� �׼�
    public static event Action<EXP> OnGetEXP;

    // ����ġ�� �������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {          
            OnGetEXP(EXPManager.Instance.HeroExp);
            Destroy(this.gameObject);
        }
    }
}
