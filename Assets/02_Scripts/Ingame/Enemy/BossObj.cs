using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BossObj : Enemy
{
    [SerializeField]
    private readonly BossData _baseBossData;    // 보스 데이터 원본

    [SerializeField]
    private GameObject groundEffectPrefab;      // 원형 범위 효과 프리팹

    private float _appearTime;                  // 보스 등장 시간
    private float _skillRadius;                 // 원형 범위의 반지름
    private float _skillDuration;               // 원형 범위가 차오르는 시간
    private float _skillDamage;                 // 스킬 데미지
    private float _skillRangeMin;               // 스킬이 생성되는 최소 범위
    private float _skillRangeMax;               // 스킬이 생성되는 최대 범위
    private float _skillCount;                  // 스킬 생성 개수
    private float _skillCoolTime;               // 스킬 쿨타임

    private List<GameObject> _activeGroundEffects = new List<GameObject>();  // 활성화된 원형 범위 효과 리스트


    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    public override void DataSetting()
    {
        _hp = _baseBossData.MaxHp;
        _atk = _baseBossData.Atk;
        _speed = _baseBossData.Speed;

        _appearTime = _baseBossData.AppearTime;
        _skillRadius = _baseBossData.SkillRadius;
        _skillDuration = _baseBossData.SkillDuration;
        _skillDamage = _baseBossData.SkillDamage;
        _skillRangeMin = _baseBossData.SkillRangeMin;
        _skillRangeMax = _baseBossData.SkillRangeMax;
        _skillCount = _baseBossData.SkillCount;
        _skillCoolTime = _baseBossData.SkillCoolTime;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.speed = _speed;

        EnemyManager.Instance.AddFieldEnemyList(this);
    }

    private void Start()
    {
        SkillInstantiate();
    }

    /// <summary>
    /// 랜덤한 위치에 _skillCount만큼 갯수의 원형범위 스킬 생성
    /// </summary>
    private void SkillInstantiate()
    {        
        for (int i = 0; i < _skillCount; i++)
        {
            Vector3 randomPos = GetRandomPositionAroundBoss();
            GameObject groundEffect = Instantiate(groundEffectPrefab, randomPos, Quaternion.identity);
            _activeGroundEffects.Add(groundEffect);
            StartCoroutine(ChargeGroundEffect(groundEffect));
        }
    }

    /// <summary>
    /// 보스 주변에 스킬을 생성할 랜덤한 위치 반환 (최소 최대 범위 내)
    /// </summary>
    private Vector3 GetRandomPositionAroundBoss()
    {
        float randomDistance = Random.Range(_skillRangeMin, _skillRangeMax);
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomPos = transform.position +
            new Vector3(
                Mathf.Cos(randomAngle) * randomDistance,
                Mathf.Sin(randomAngle) * randomDistance,
                0);

        return randomPos;
    }

    /// <summary>
    /// 원형범위스킬 차오르는 효과
    /// </summary>
    private IEnumerator ChargeGroundEffect(GameObject groundEffect)
    {
        float elapsedTime = 0f;
        Vector3 originalScale = groundEffect.transform.localScale;
        Vector3 targetScale = new Vector3(_skillRadius * 2, _skillRadius * 2, 1f);

        while (elapsedTime < _skillDuration)
        {
            elapsedTime += Time.deltaTime;
            groundEffect.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / _skillDuration);
            yield return null;
        }

        // 다 차오른 후 데미지 판정 시작
        groundEffect.GetComponent<GroundEffect>().ActivateDamage();
    }

    /// <summary>
    /// 죽음
    /// </summary>
    public override void Death()
    {
        base.Death();

        Destroy(this.gameObject);
    }
}
