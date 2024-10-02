using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : SerializedMonoBehaviour
{
    #region 싱글톤
    private static MonsterFactory _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
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


    // 몬스터 데이터 셋팅해서 리턴
    public MonsterObject SettingMonster(MonsterID monsterID)
    {
        // 오브젝트 풀에서 몬스터 가져옴
        GameObject go = _monsterObjectPoolDict[monsterID].GetObj();

        // 몬스터 데이터 초기화 (셋팅)
        MonsterObject monsterobject = go.GetComponent<MonsterObject>();
        monsterobject.DataSetting();

        // 그 몬스터를 반환
        return monsterobject;
    }
}
