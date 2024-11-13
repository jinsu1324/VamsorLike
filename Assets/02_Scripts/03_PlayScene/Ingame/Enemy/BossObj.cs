using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BossObj : Enemy
{
    [SerializeField]
    private BossID _bossID;                                     // �� ������Ʈ�� ���� ID

    private BossData _baseBossData;                             // ���� ������

    [SerializeField]
    private GroundSkill _groundSkillPrefab;                     // ���� ���� ��ų ������

    private float _appearStageLevel;                            // ���� ���� �������� ����
    private float _skillRadius;                                 // ��ų�� ������
    private float _skillDuration;                               // ��ų�� �������� �ð�
    private float _skillDamage;                                 // ��ų ������
    private float _skillRangeMin;                               // ��ų�� �����Ǵ� �ּ� ����
    private float _skillRangeMax;                               // ��ų�� �����Ǵ� �ִ� ����
    private float _skillCount;                                  // ��ų ���� ����
    private float _skillCoolTime;                               // ��ų ��Ÿ��

    private List<GroundSkill> _activeGroundSkillsList = new List<GroundSkill>();  // Ȱ��ȭ�� �������� ��ų ����Ʈ

    private float _time;                                        // ��ų ��Ÿ�� ����� �ð� ����

    
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
    /// ������ ����
    /// </summary>
    public override void DataSetting()
    {
        _baseBossData = DataManager.Instance.BossDatas.GetDataById(_bossID.ToString());

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

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>(); 

        PlaySceneManager.Instance.EnemyManager.AddFieldEnemyList(this);
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    public override void DropItem()
    {
        PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.RewardBox, transform.position);
    }

    /// <summary>
    /// HP ����
    /// </summary>
    public override void HPMinus(float atk)
    {
        base.HPMinus(atk);

        PlaySceneCanvas.Instance.BossHPBarUI.Refresh_BossHPBar(_hp, _baseBossData.MaxHp);

        if (_hp <= 0)
            Death();
    }

    /// <summary>
    /// ����
    /// </summary>
    public override void Death()
    {
        base.Death();

        PlaySceneCanvas.Instance.BossHPBarUI.Popup_OFF();
    }

    /// <summary>
    /// ��ų ��Ÿ��
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
    /// ������ ��ġ�� _skillCount��ŭ ������ �������� ��ų ����
    /// </summary>
    private void SkillStart()
    {
        _animator.SetTrigger("isSkill");

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
    /// ���� �ֺ��� ��ų�� ������ ������ ��ġ ��ȯ (�ּ� �ִ� ���� ��)
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
