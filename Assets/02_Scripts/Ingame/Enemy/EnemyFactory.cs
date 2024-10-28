using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : SerializedMonoBehaviour
{
    // 몬스터 오브젝트 풀 딕셔너리
    [SerializeField]
    private Dictionary<MonsterID, ObjectPool> _monsterObjectPoolDict = new Dictionary<MonsterID, ObjectPool>();

    // 보스 오브젝트 풀 딕셔너리
    [SerializeField]
    private Dictionary<BossID, ObjectPool> _bossObjectPoolDict = new Dictionary<BossID, ObjectPool>();


    /// <summary>
    /// 몬스터 데이터 셋팅해서 리턴
    /// </summary>
    public MonsterObj SettingMonster(MonsterID monsterID)
    {
        // 오브젝트 풀에서 몬스터 가져옴
        GameObject go = _monsterObjectPoolDict[monsterID].GetObj();

        // 몬스터 데이터 초기화 (셋팅)
        MonsterObj monster = go.GetComponent<MonsterObj>();
        monster.DataSetting();

        // 그 몬스터를 반환
        return monster;
    }

    /// <summary>
    /// 보스 데이터 셋팅해서 리턴
    /// </summary>
    public BossObj SettingMonster(BossID bossID)
    {
        // 오브젝트 풀에서 보스 가져옴
        GameObject go = _bossObjectPoolDict[bossID].GetObj();

        // 보스 데이터 초기화 (셋팅)
        BossObj boss = go.GetComponent<BossObj>();
        boss.DataSetting();

        // 그 보스를 반환
        return boss;
    }
}
