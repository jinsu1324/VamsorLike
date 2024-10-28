using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBoxItem : MonoBehaviour
{
    // 리워드상자 획득했을 때 액션
    public static event Action OnGetRewardBox;

    // 상자에 닿았을때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            OnGetRewardBox?.Invoke();

            Destroy(this.gameObject);
        }
    }
}
