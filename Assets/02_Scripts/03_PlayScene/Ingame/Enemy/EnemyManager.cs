using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : SerializedMonoBehaviour
{
    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // 필드에 스폰되어있는 적 리스트

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (PlaySceneManager.Instance.IsGameStart == false)
            return;

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
    /// 거리 내 모든 적들을 찾아서 반환
    /// </summary>
    public List<Enemy> Get_AllEnemy_In_Distance(Vector3 pos, float distance)
    {
        List<Enemy> enemyList = new List<Enemy>();
        for (int i = 0; i < _fieldEnemyList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldEnemyList[i].transform.position))
            {
                enemyList.Add(_fieldEnemyList[i]);                
            }
        }

        return enemyList;
    }

    /// <summary>
    /// 거리 내 적 리스트 중에서, 플레이어에게 가장 가까운 n마리를 반환
    /// </summary>
    public List<Enemy> Get_ClosestEnemys_In_Distance(Vector3 pos, float distance, int count)
    {
        // 거리 내 모든 적들을 찾아서 저장
        List<Enemy> enemyList = Get_AllEnemy_In_Distance(pos, distance);

        // 거리 내 적들을 거리순으로 정렬
        enemyList.Sort(
            (enemy1, enemy2) =>
            {
                float dist1 = Vector3.Distance(pos, enemy1.transform.position);
                float dist2 = Vector3.Distance(pos, enemy2.transform.position);
                return dist1.CompareTo(dist2);
            });

        // 가까운 n마리 반환 (n이 리스트 크기보다 클 경우, 리스트 전체 반환)
        return enemyList.Take(count).ToList();
    }

    /// <summary>
    /// 거리 내 적 리스트 중에서, 랜덤한 n마리를 반환
    /// </summary>
    public List<Enemy> Get_RandomEnemys_In_Distance(Vector3 pos, float distance, int count)
    {
        // 거리 내 모든 적들을 찾아서 저장
        List<Enemy> enemyList = Get_AllEnemy_In_Distance(pos, distance);

        // 적 리스트를 무작위로 섞음
        System.Random random = new System.Random();
        enemyList = enemyList.OrderBy(enemy => random.Next()).ToList();

        // 가까운 n마리 반환 (n이 리스트 크기보다 클 경우, 리스트 전체 반환)
        return enemyList.Take(count).ToList();
    }

    /// <summary>
    /// 필드 적들 전부 영웅 따라다니도록 
    /// </summary>
    private void FollowHero_AllEnemys()
    {
        foreach (Enemy enemy in _fieldEnemyList)
            enemy.FollowHero();
    }
}
