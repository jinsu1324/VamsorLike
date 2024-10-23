using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// ���� ���� ���� ������Ʈ
/// </summary>
public class MonsterObject : MonsterObjectBase
{
    public static event Action<MonsterObject> OnMonsterDeath;   // ���� �׾����� ó���� �Լ��� �׼�

    [SerializeField]
    private readonly MonsterData _baseMonsterData;              // ���� ������Ʈ�� �� ������ 

    
    /// <summary>
    /// ������ ����
    /// </summary>
    public override void DataSetting()
    {
        Hp = _baseMonsterData.MaxHp;
        Atk = _baseMonsterData.Atk;
        Speed = _baseMonsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.speed = Speed;

        MonsterManager.Instance.AddFieldMonsterList(this);
    }

    /// <summary>
    /// ����
    /// </summary>
    public override void Death()
    {
        // ���� �׾����� �׼ǵ� ���� (�ʵ� ���� ����Ʈ���� �� ���� ���� / �ٴڿ� ����ġ ������ / �ٽ� ������Ʈ Ǯ�� ����������)
        OnMonsterDeath?.Invoke(this);

        Destroy(this.gameObject);
    }    
}
