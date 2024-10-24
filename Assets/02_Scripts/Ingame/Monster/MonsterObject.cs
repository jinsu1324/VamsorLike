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
    [SerializeField]
    private readonly MonsterData _baseMonsterData;              // ���� ������ ����
    
    /// <summary>
    /// ������ ����
    /// </summary>
    public override void DataSetting()
    {
        _hp = _baseMonsterData.MaxHp;
        _atk = _baseMonsterData.Atk;
        _speed = _baseMonsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.speed = _speed;

        MonsterManager.Instance.AddFieldMonsterList(this);
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
