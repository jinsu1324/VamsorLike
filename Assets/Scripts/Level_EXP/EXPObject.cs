using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPObject : MonoBehaviour
{
    // ����ġ ȹ������ �� �׼�
    public static event Action OnGetExp;


    // ����ġ�� �������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            OnGetExp();
            Destroy(this.gameObject);
        }
    }
}
