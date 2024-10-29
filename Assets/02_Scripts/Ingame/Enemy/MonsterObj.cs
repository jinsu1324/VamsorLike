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
public class MonsterObj : Enemy
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

        PlaySceneManager.Instance.EnemyManager.AddFieldEnemyList(this);
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    public override void DropItem()
    {
        PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.EXP, transform.position);
        PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.Gold, transform.position + new Vector3(0, 0.1f, 0));
    }

    /// <summary>
    /// HP ����
    /// </summary>
    public override void HPMinus(float atk)
    {
        base.HPMinus(atk);

        if (_hp <= 0)
            Death();
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
