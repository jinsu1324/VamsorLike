using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffect : MonoBehaviour
{
    private bool _isDamageActive = false;  // ������ ���� Ȱ��ȭ ����
    public float damageAmount = 20f;       // �÷��̾�� ���� ������

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
            Debug.Log($"���� ������ : {damageAmount}");
            playerCollider.GetComponent<HeroObj>().HPMinus(damageAmount);
        }
    }
}
