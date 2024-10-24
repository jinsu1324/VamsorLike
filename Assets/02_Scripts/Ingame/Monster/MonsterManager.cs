using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SerializedMonoBehaviour
{
    #region 싱글톤_씬이동x
    private static MonsterManager _instance;

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
        
    // 필드에 스폰되어있는 몬스터 리스트
    private List<MonsterObjectBase> _fieldMonsterList = new List<MonsterObjectBase>();

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        MonsterObjectBase.OnMonsterDeath += RemoveFieldMonsterList;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (PlaySceneManager.Instance.IsGameStart == false)
        {
            Stop_AllMonster();
            return;
        }            

        FollowHero_AllMonster();
    }    

    /// <summary>
    /// 필드몬스터 리스트에 추가
    /// </summary>
    public void AddFieldMonsterList(MonsterObjectBase monster)
    {
        _fieldMonsterList.Add(monster);
    }

    /// <summary>
    /// 필드몬스터 리스트에서 삭제
    /// </summary>
    public void RemoveFieldMonsterList(MonsterObjectBase monster)
    {
        _fieldMonsterList.Remove(monster);
    }

    /// <summary>
    /// 일정 거리 내 몬스터의 리스트를 받아옴
    /// </summary>
    public List<MonsterObjectBase> GetMonstersByDistance(Vector3 pos, float distance)
    {
        List<MonsterObjectBase> closeMonsterList = new List<MonsterObjectBase>();

        for (int i = 0; i < _fieldMonsterList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldMonsterList[i].transform.position))
            {
                closeMonsterList.Add(_fieldMonsterList[i]);                
            }
        }

        return closeMonsterList;
    }

    /// <summary>
    /// 일정 거리 내 몬스터 리스트 중에서, 플레이어에게 가장 가까운 몬스터 1마리를 반환
    /// </summary>
    public MonsterObjectBase GetClosestMonstersByDistance(Vector3 pos, float distance)
    {
        // 사거리 내 몬스터 모두 리스트에 저장
        List<MonsterObjectBase> closeMonsterList = new List<MonsterObjectBase>();

        for (int i = 0; i < _fieldMonsterList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldMonsterList[i].transform.position))
            {
                closeMonsterList.Add(_fieldMonsterList[i]);
            }
        }

        // 그 리스트에서 가장 가까운 몬스터 반환
        MonsterObjectBase closestMonster = null;
        float maxDistance = distance;

        foreach (MonsterObjectBase monster in closeMonsterList)
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

    /// <summary>
    /// 필드몬스터들 전부 영웅 따라다니도록 
    /// </summary>
    private void FollowHero_AllMonster()
    {
        foreach (MonsterObjectBase monster in _fieldMonsterList)
            monster.FollowHero();
    }

    /// <summary>
    /// 필드몬스터들 전부 멈추도록
    /// </summary>
    private void Stop_AllMonster()
    {
        foreach (MonsterObjectBase monster in _fieldMonsterList)
            monster.StopFollow();
    }

    /// <summary>
    /// 씬 전환되거나 오브젝트 파괴될 때 이벤트 제거
    /// </summary>
    public void OnDisable()
    {
        MonsterObjectBase.OnMonsterDeath -= RemoveFieldMonsterList;
    }
}
