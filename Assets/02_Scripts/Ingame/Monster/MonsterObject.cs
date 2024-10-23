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
public class MonsterObject : MonsterObjectBase
{
    public static event Action<MonsterObject> OnMonsterDeath;   // 몬스터 죽었을때 처리될 함수들 액션

    [SerializeField]
    private readonly MonsterData _baseMonsterData;              // 몬스터 오브젝트에 들어갈 데이터 

    
    /// <summary>
    /// 데이터 셋팅
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
    /// 죽음
    /// </summary>
    public override void Death()
    {
        // 몬스터 죽었을때 액션들 실행 (필드 몬스터 리스트에서 이 몬스터 삭제 / 바닥에 경험치 떨구기 / 다시 오브젝트 풀로 돌려보내기)
        OnMonsterDeath?.Invoke(this);

        Destroy(this.gameObject);
    }    
}
