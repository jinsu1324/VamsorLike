using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BossObj : Enemy
{
    public static event Action<float, float> OnBossHPChanged;   // 보스 HP 변경되었을 때 이벤트

    [SerializeField]
    private readonly BossData _baseBossData;                    // 보스 데이터 원본

    [SerializeField]
    private GroundSkill _groundSkillPrefab;                     // 원형 범위 스킬 프리팹

    private float _appearStageLevel;                            // 보스 등장 스테이지 레벨
    private float _skillRadius;                                 // 스킬의 반지름
    private float _skillDuration;                               // 스킬이 차오르는 시간
    private float _skillDamage;                                 // 스킬 데미지
    private float _skillRangeMin;                               // 스킬이 생성되는 최소 범위
    private float _skillRangeMax;                               // 스킬이 생성되는 최대 범위
    private float _skillCount;                                  // 스킬 생성 개수
    private float _skillCoolTime;                               // 스킬 쿨타임

    private List<GroundSkill> _activeGroundSkillsList = new List<GroundSkill>();  // 활성화된 원형범위 스킬 리스트

    private float _time;                                        // 스킬 쿨타임 계산할 시간 변수

    
    /// <summary>
    /// FixedUpdate
    /// </summary>
    private void FixedUpdate()
    {
        if (PlaySceneManager.Instance.IsGameStart == false)
            return;

        if (SkillCooltime())
            SkillStart();
    }

    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    public override void DataSetting()
    {
        _hp = _baseBossData.MaxHp;
        _atk = _baseBossData.Atk;
        _speed = _baseBossData.Speed;

        _appearStageLevel = _baseBossData.AppearStageLevel;
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

    /// <summary>
    /// HP 감소
    /// </summary>
    public override void HPMinus(float atk)
    {
        base.HPMinus(atk);

        OnBossHPChanged(_hp, _baseBossData.MaxHp);

        if (_hp <= 0)
            Death();
    }

    /// <summary>
    /// 죽음
    /// </summary>
    public override void Death()
    {
        base.Death();
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 스킬 쿨타임
    /// </summary>
    private bool SkillCooltime()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _skillCoolTime)
        {
            _time %= _skillCoolTime;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 랜덤한 위치에 _skillCount만큼 갯수의 원형범위 스킬 생성
    /// </summary>
    private void SkillStart()
    {        
        for (int i = 0; i < _skillCount; i++)
        {
            Vector3 randomPos = GetRandomPositionAroundBoss();
            GroundSkill groundSkill = Instantiate(_groundSkillPrefab, randomPos, Quaternion.identity);
            _activeGroundSkillsList.Add(groundSkill);

            GroundSkillArgs groundSkillArgs = new GroundSkillArgs
            {
                skillDamage = _skillDamage,
                skillRadius = _skillRadius,
                skillDuration = _skillDuration
            };

            groundSkill.Start_ChargeGroundEffect(groundSkillArgs);
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
}
