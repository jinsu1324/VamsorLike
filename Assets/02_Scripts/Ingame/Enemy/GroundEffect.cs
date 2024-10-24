using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffect : MonoBehaviour
{
    private bool _isDamageActive = false;  // 데미지 판정 활성화 여부
    public float damageAmount = 20f;       // 플레이어에게 입힐 데미지

    private void Update()
    {
        if (_isDamageActive)
        {
            CheckPlayerInRange();
        }
    }

    public void ActivateDamage()
    {
        _isDamageActive = true;
    }


    private void CheckPlayerInRange()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(
            transform.position, 
            transform.localScale.x / 2, 
            LayerMask.GetMask("Hero"));

        if (playerCollider != null)
        {
            Debug.Log($"입힌 데미지 : {damageAmount}");
            playerCollider.GetComponent<HeroObj>().HPMinus(damageAmount);
        }
    }
}
