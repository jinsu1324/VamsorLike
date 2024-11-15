using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            return;

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
    /// �Ÿ� �� ��� ������ ã�Ƽ� ��ȯ
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
    /// �Ÿ� �� �� ����Ʈ �߿���, �÷��̾�� ���� ����� n������ ��ȯ
    /// </summary>
    public List<Enemy> Get_ClosestEnemys_In_Distance(Vector3 pos, float distance, int count)
    {
        // �Ÿ� �� ��� ������ ã�Ƽ� ����
        List<Enemy> enemyList = Get_AllEnemy_In_Distance(pos, distance);

        // �Ÿ� �� ������ �Ÿ������� ����
        enemyList.Sort(
            (enemy1, enemy2) =>
            {
                float dist1 = Vector3.Distance(pos, enemy1.transform.position);
                float dist2 = Vector3.Distance(pos, enemy2.transform.position);
                return dist1.CompareTo(dist2);
            });

        // ����� n���� ��ȯ (n�� ����Ʈ ũ�⺸�� Ŭ ���, ����Ʈ ��ü ��ȯ)
        return enemyList.Take(count).ToList();
    }

    /// <summary>
    /// �Ÿ� �� �� ����Ʈ �߿���, ������ n������ ��ȯ
    /// </summary>
    public List<Enemy> Get_RandomEnemys_In_Distance(Vector3 pos, float distance, int count)
    {
        // �Ÿ� �� ��� ������ ã�Ƽ� ����
        List<Enemy> enemyList = Get_AllEnemy_In_Distance(pos, distance);

        // �� ����Ʈ�� �������� ����
        System.Random random = new System.Random();
        enemyList = enemyList.OrderBy(enemy => random.Next()).ToList();

        // ����� n���� ��ȯ (n�� ����Ʈ ũ�⺸�� Ŭ ���, ����Ʈ ��ü ��ȯ)
        return enemyList.Take(count).ToList();
    }

    /// <summary>
    /// �ʵ� ���� ���� ���� ����ٴϵ��� 
    /// </summary>
    private void FollowHero_AllEnemys()
    {
        foreach (Enemy enemy in _fieldEnemyList)
            enemy.FollowHero();
    }
}
