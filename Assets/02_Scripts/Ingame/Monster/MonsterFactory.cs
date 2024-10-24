using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : SerializedMonoBehaviour
{
    #region 싱글톤_씬이동x
    private static MonsterFactory _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static MonsterFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion

    // 몬스터 오브젝트 풀 딕셔너리
    [SerializeField]
    private Dictionary<MonsterID, ObjectPool> _monsterObjectPoolDict = new Dictionary<MonsterID, ObjectPool>();

    // 보스 오브젝트 풀
    [SerializeField]
    private Dictionary<BossID, ObjectPool> _bossObjectPoolDict = new Dictionary<BossID, ObjectPool>();

    /// <summary>
    /// 몬스터 데이터 셋팅해서 리턴
    /// </summary>
    public MonsterObject SettingMonster(MonsterID monsterID)
    {
        // 오브젝트 풀에서 몬스터 가져옴
        GameObject go = _monsterObjectPoolDict[monsterID].GetObj();

        // 몬스터 데이터 초기화 (셋팅)
        MonsterObject monster = go.GetComponent<MonsterObject>();
        monster.DataSetting();

        // 그 몬스터를 반환
        return monster;
    }

    /// <summary>
    /// 보스 데이터 셋팅해서 리턴
    /// </summary>
    public BossObject SettingMonster(BossID bossID)
    {
        // 오브젝트 풀에서 보스 가져옴
        GameObject go = _bossObjectPoolDict[bossID].GetObj();

        // 보스 데이터 초기화 (셋팅)
        BossObject boss = go.GetComponent<BossObject>();
        boss.DataSetting();

        // 그 보스를 반환
        return boss;
    }
}
