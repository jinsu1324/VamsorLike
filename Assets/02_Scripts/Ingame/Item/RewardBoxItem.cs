using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBoxItem : MonoBehaviour
{
    // ��������� ȹ������ �� �׼�
    public static event Action OnGetRewardBox;

    // ���ڿ� �������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            OnGetRewardBox?.Invoke();

            Destroy(this.gameObject);
        }
    }
}
