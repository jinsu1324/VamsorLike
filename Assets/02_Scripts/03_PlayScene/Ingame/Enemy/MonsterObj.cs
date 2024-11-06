using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// 실제 몬스터 게임 오브젝트
/// </summary>
public class MonsterObj : Enemy
{
    [SerializeField]
    private MonsterID _monsterID;                               // 이 오브젝트의 몬스터 ID

    private MonsterData _baseMonsterData;                       // 몬스터 데이터
    
    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    public override void DataSetting()
    {
        _baseMonsterData = DataManager.Instance.MonsterDatas.GetDataById(_monsterID.ToString());

        _hp = _baseMonsterData.MaxHp;
        _atk = _baseMonsterData.Atk;
        _speed = _baseMonsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        PlaySceneManager.Instance.EnemyManager.AddFieldEnemyList(this);
    }

    /// <summary>
    /// 아이템 드랍
    /// </summary>
    public override void DropItem()
    {
        PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.EXP, transform.position);
        //PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.Gold, transform.position);
        //PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.Heal, transform.position);
        //PlaySceneManager.Instance.ItemSpawner.SpawnItem(ItemID.Magnet, transform.position);
    }

    /// <summary>
    /// HP 감소
    /// </summary>
    public override void HPMinus(float atk)
    {
        base.HPMinus(atk);

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
}
