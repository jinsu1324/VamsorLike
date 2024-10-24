using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GroundSkill에 필요한 정보들 구조체
/// </summary>
public struct GroundSkillArgs
{
    public float skillDamage;
    public float skillRadius;
    public float skillDuration;
}


/// <summary>
/// 장판 스킬 관리 클래스
/// </summary>
public class GroundSkill : SerializedMonoBehaviour
{
    private bool _isChargeFinished = false;       // 스킬 차지효과 끝났는지
    private float _damage;                        // 플레이어에게 입힐 데미지

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
    /// 바닥차는 효과 코루틴시작
    /// </summary>
    public void Start_ChargeGroundEffect(GroundSkillArgs groundSkillArgs)
    {
        _damage = groundSkillArgs.skillDamage;
        float skillRadius = groundSkillArgs.skillRadius;
        float skillDuration = groundSkillArgs.skillDuration;

        StartCoroutine(ChargeGroundEffect(skillRadius, skillDuration));
    }

    /// <summary>
    /// 원형범위스킬 차오르는 효과 코루틴
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
    /// 영웅이 범위에 있는지 확인 후 데미지 입히기
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
    /// 스킬 끝났을 때 호출
    /// </summary>
    public void SkillFinish()
    {
        Change_IsChargeFinished(false);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 스킬이 데미지를 줄 준비가 되었는지 여부 변경
    /// </summary>
    public void Change_IsChargeFinished(bool isChargeFinished)
    {
        _isChargeFinished = isChargeFinished;
    }
}
