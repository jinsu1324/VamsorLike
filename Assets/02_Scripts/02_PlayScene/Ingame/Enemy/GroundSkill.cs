using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GroundSkill�� �ʿ��� ������ ����ü
/// </summary>
public struct GroundSkillArgs
{
    public float skillDamage;
    public float skillRadius;
    public float skillDuration;
}


/// <summary>
/// ���� ��ų ���� Ŭ����
/// </summary>
public class GroundSkill : SerializedMonoBehaviour
{
    private bool _isChargeFinished = false;       // ��ų ����ȿ�� ��������
    private float _damage;                        // �÷��̾�� ���� ������

    [SerializeField]
    public GameObject BaseCircle { get; set; }
    [SerializeField]
    public GameObject ChangeCircle { get; set; }

    /// <summary>
    /// FixedUpdate
    /// </summary>
    private void FixedUpdate()
    {
        if (_isChargeFinished == false)
            return;
        
        CheckInHero_and_Damage();
    }

    /// <summary>
    /// �ٴ����� ȿ�� �ڷ�ƾ����
    /// </summary>
    public void Start_ChargeGroundEffect(GroundSkillArgs groundSkillArgs)
    {
        _damage = groundSkillArgs.skillDamage;
        float skillRadius = groundSkillArgs.skillRadius;
        float skillDuration = groundSkillArgs.skillDuration;

        StartCoroutine(ChargeGroundEffect(skillRadius, skillDuration));
    }

    /// <summary>
    /// ����������ų �������� ȿ�� �ڷ�ƾ
    /// </summary>
    private IEnumerator ChargeGroundEffect(float skillRadius, float skillDuration)
    {
        Change_IsChargeFinished(false);

        float elapsedTime = 0f;

        Vector3 originalScale = ChangeCircle.transform.localScale;
        Vector3 targetScale = new Vector3(skillRadius * 2, skillRadius * 2, 1f);

        BaseCircle.transform.localScale = targetScale;

        while (elapsedTime < skillDuration)
        {
            elapsedTime += Time.deltaTime;
            ChangeCircle.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / skillDuration);
            yield return null;
        }

        Change_IsChargeFinished(true);
    }    

    /// <summary>
    /// ������ ������ �ִ��� Ȯ�� �� ������ ������
    /// </summary>
    private void CheckInHero_and_Damage()
    {
        Collider2D heroCollider = Physics2D.OverlapCircle(
            transform.position, 
            transform.localScale.x / 2, 
            LayerMask.GetMask("Hero"));

        if (heroCollider != null)
        {
            heroCollider.GetComponent<HeroObj>().HPMinus(_damage);
        }

        SkillFinish();
    }

    /// <summary>
    /// ��ų ������ �� ȣ��
    /// </summary>
    public void SkillFinish()
    {
        Change_IsChargeFinished(false);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// ��ų�� �������� �� �غ� �Ǿ����� ���� ����
    /// </summary>
    public void Change_IsChargeFinished(bool isChargeFinished)
    {
        _isChargeFinished = isChargeFinished;
    }
}
