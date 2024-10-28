using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SerializedMonoBehaviour
{
    private List<Enemy> _fieldEnemyList = new List<Enemy>();    // �ʵ忡 �����Ǿ��ִ� �� ����Ʈ

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
    /// �ʵ� �� ����Ʈ�� �߰�
    /// </summary>
    public void AddFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Add(enemy);
    }

    /// <summary>
    /// �ʵ� �� ����Ʈ���� ����
    /// </summary>
    public void RemoveFieldEnemyList(Enemy enemy)
    {
        _fieldEnemyList.Remove(enemy);
    }

    /// <summary>
    /// ���� �Ÿ� �� ������ ����Ʈ�� �޾ƿ�
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
    /// ���� �Ÿ� �� �� ����Ʈ �߿���, �÷��̾�� ���� ����� �� 1������ ��ȯ
    /// </summary>
    public Enemy Get_ClosestEnemy_In_Distance(Vector3 pos, float distance)
    {
        // ��Ÿ� �� ������ ������ ����Ʈ
        List<Enemy> getEnemyList = new List<Enemy>();

        for (int i = 0; i < _fieldEnemyList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldEnemyList[i].transform.position))
            {
                getEnemyList.Add(_fieldEnemyList[i]);
            }
        }

        // �� ����Ʈ���� ���� ����� ���� ��ȯ
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
    /// �ʵ� ���� ���� ���� ����ٴϵ��� 
    /// </summary>
    private void FollowHero_AllEnemys()
    {
        foreach (Enemy enemy in _fieldEnemyList)
            enemy.FollowHero();
    }

    /// <summary>
    /// �ʵ� ���� ���� ���ߵ���
    /// </summary>
    private void Stop_AllEnemys()
    {
        foreach (Enemy enemy in _fieldEnemyList)
            enemy.Stop();
    }
}
