using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BossObj : Enemy
{
    [SerializeField]
    private readonly BossData _baseBossData;    // ���� ������ ����

    [SerializeField]
    private GameObject groundEffectPrefab;      // ���� ���� ȿ�� ������

    private float _appearTime;                  // ���� ���� �ð�
    private float _skillRadius;                 // ���� ������ ������
    private float _skillDuration;               // ���� ������ �������� �ð�
    private float _skillDamage;                 // ��ų ������
    private float _skillRangeMin;               // ��ų�� �����Ǵ� �ּ� ����
    private float _skillRangeMax;               // ��ų�� �����Ǵ� �ִ� ����
    private float _skillCount;                  // ��ų ���� ����
    private float _skillCoolTime;               // ��ų ��Ÿ��

    private List<GameObject> _activeGroundEffects = new List<GameObject>();  // Ȱ��ȭ�� ���� ���� ȿ�� ����Ʈ


    /// <summary>
    /// ������ ����
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
    /// ������ ��ġ�� _skillCount��ŭ ������ �������� ��ų ����
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

    /// <summary>
    /// ����������ų �������� ȿ��
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

        // �� ������ �� ������ ���� ����
        groundEffect.GetComponent<GroundEffect>().ActivateDamage();
    }

    /// <summary>
    /// ����
    /// </summary>
    public override void Death()
    {
        base.Death();

        Destroy(this.gameObject);
    }
}
