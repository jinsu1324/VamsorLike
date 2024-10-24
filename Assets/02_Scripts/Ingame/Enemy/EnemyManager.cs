using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SerializedMonoBehaviour
{
    #region 싱글톤_씬이동x
    private static EnemyManager _instance;

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

    public static EnemyManager Instance
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
        
    // 필드에 스폰되어있는 적 리스트
    private List<Enemy> _fieldEnemyList = new List<Enemy>();

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Enemy.OnEnemyDead += RemoveFieldEnemyList;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (PlaySceneManager.Instance.IsGameStart == false)
        {
            Stop_AllEnemys();
            return;
        }            

        FollowHero_AllEnemys();
    }    

    /// <summary>
    /// 필드 적 리스트에 추가
    /// </summary>
    public void AddFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Add(enemy);
    }

    /// <summary>
    /// 필드 적 리스트에서 삭제
    /// </summary>
    public void RemoveFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Remove(enemy);
    }

    /// <summary>
    /// 일정 거리 내 적들을 리스트로 받아옴
    /// </summary>
    public List<Enemy> Get_AllEnemy_In_Distance(Vector3 pos, float distance)
    {
        List<Enemy> getEnemyList = new List<Enemy>();

        for (int i = 0; i < _fieldEnemyList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldEnemyList[i].transform.position))
            {
                getEnemyList.Add(_fieldEnemyList[i]);                
            }
        }

        return getEnemyList;
    }

    /// <summary>
    /// 일정 거리 내 적 리스트 중에서, 플레이어에게 가장 가까운 적 1마리를 반환
    /// </summary>
    public Enemy Get_ClosestEnemy_In_Distance(Vector3 pos, float distance)
    {
        // 사거리 내 적들을 저장할 리스트
        List<Enemy> getEnemyList = new List<Enemy>();

        for (int i = 0; i < _fieldEnemyList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldEnemyList[i].transform.position))
            {
                getEnemyList.Add(_fieldEnemyList[i]);
            }
        }

        // 그 리스트에서 가장 가까운 적을 반환
        Enemy closestEnemy = null;
        float maxDistance = distance;

        foreach (Enemy enemy in getEnemyList)
        {
            float targetDistance = Vector3.Distance(pos, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestEnemy = enemy;
                maxDistance = targetDistance;
            }
        }

        return closestEnemy;
    }

    /// <summary>
    /// 필드 적들 전부 영웅 따라다니도록 
    /// </summary>
    private void FollowHero_AllEnemys()
    {
        foreach (Enemy enemy in _fieldEnemyList)
            enemy.FollowHero();
    }

    /// <summary>
    /// 필드 적들 전부 멈추도록
    /// </summary>
    private void Stop_AllEnemys()
    {
        foreach (Enemy enemy in _fieldEnemyList)
            enemy.Stop();
    }

    /// <summary>
    /// 씬 전환되거나 오브젝트 파괴될 때 이벤트 제거
    /// </summary>
    public void OnDisable()
    {
        Enemy.OnEnemyDead -= RemoveFieldEnemyList;
    }
}
