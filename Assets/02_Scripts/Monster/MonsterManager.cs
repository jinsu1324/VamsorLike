using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SerializedMonoBehaviour
{
    #region 싱글톤
    private static MonsterManager _instance;

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

    public static MonsterManager Instance
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

    // 몬스터 데이터 딕셔너리
    [SerializeField]
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get; set; } = new Dictionary<MonsterID, MonsterData>();


    // 몬스터 오브젝트들 딕셔너리
    [SerializeField]
    public Dictionary<MonsterID, MonsterObject> MonsterObjectDict { get; set; } = new Dictionary<MonsterID, MonsterObject>();


    // 필드에 스폰되어있는 몬스터 리스트
    private List<MonsterObject> _fieldMonsterList = new List<MonsterObject>();

    private void Start()
    {
        MonsterObject.OnMonsterDeath += RemoveFieldMonsterList;
    }

    private void Update()
    {
        if (PlaySceneManager.Instance.IsGameStart)
        {
            AllFieldMonsterFollowHero();
        }        
    }    

    // 필드몬스터 리스트에 추가
    public void AddFieldMonsterList(MonsterObject monsterObject)
    {
        _fieldMonsterList.Add(monsterObject);
    }

    // 필드몬스터 리스트에서 삭제
    public void RemoveFieldMonsterList(MonsterObject monsterObject)
    {
        _fieldMonsterList.Remove(monsterObject);
    }

    // 일정 거리 내 몬스터의 리스트를 받아옴
    public List<MonsterObject> GetMonstersByDistance(Vector3 pos, float distance)
    {
        List<MonsterObject> closeMonsterList = new List<MonsterObject>();

        for (int i = 0; i < _fieldMonsterList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldMonsterList[i].transform.position))
            {
                closeMonsterList.Add(_fieldMonsterList[i]);                
            }
        }

        return closeMonsterList;
    }

    // 일정 거리 내 몬스터 리스트 중에서, 플레이어에게 가장 가까운 몬스터 1마리를 반환
    public MonsterObject GetClosestMonstersByDistance(Vector3 pos, float distance)
    {
        // 사거리 내 몬스터 모두 리스트에 저장
        List<MonsterObject> closeMonsterList = new List<MonsterObject>();

        for (int i = 0; i < _fieldMonsterList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldMonsterList[i].transform.position))
            {
                closeMonsterList.Add(_fieldMonsterList[i]);
            }
        }

        // 그 리스트에서 가장 가까운 몬스터 반환
        MonsterObject closestMonster = null;
        float maxDistance = distance;

        foreach (MonsterObject monster in closeMonsterList)
        {
            float targetDistance = Vector3.Distance(pos, monster.transform.position);

            if (targetDistance < maxDistance)
            {
                closestMonster = monster;
                maxDistance = targetDistance;
            }
        }

        return closestMonster;
    }


    // 필드몬스터들 전부 영웅 따라다니도록 
    private void AllFieldMonsterFollowHero()
    {
        foreach (MonsterObject monster in _fieldMonsterList)
            monster.FollowHero();
    }
}
