using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : MonoBehaviour
{
    // ��� ȹ������ �� �׼�
    public static event Action OnGetGold;


    // ��忡 �������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            OnGetGold?.Invoke();

            Destroy(this.gameObject);
        }
    }
}
