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
    private MonsterID _monsterID;                               // �� ������Ʈ�� ���� ID

    private MonsterData _baseMonsterData;                       // ���� ������
    
    /// <summary>
    /// ������ ����
    /// </summary>
    public override void DataSetting()
    {
        _baseMonsterData = DataManager.Instance.MonsterDatas.GetDataById(_monsterID.ToString());

        _hp = _baseMonsterData.MaxHp;
        _atk = _baseMonsterData.Atk;
        _speed = _baseMonsterData.Speed;

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.enabled = true;

        PlaySceneManager.Instance.EnemyManager.AddFieldEnemyList(this);
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    public override void DropItem()
    {
        PlaySceneManager.Instance.ItemSpawner.SpawnRandomItem(transform.position);
        //PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.EXP, transform.position);
        //PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.Gold, transform.position);
        //PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.Heal, transform.position);
        //PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.Magnet, transform.position);
        //PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.RewardBox, transform.position);
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
}
