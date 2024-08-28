using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SerializedMonoBehaviour
{
    // �ʵ忡 �����Ǿ��ִ� ���� ����Ʈ
    private List<MonsterObject> _fieldMonsterList = new List<MonsterObject>();

    private void Start()
    {
        MonsterObject.OnMonsterDeath += RemoveFieldMonsterList;
    }

    private void Update()
    {
        AllFieldMonsterFollowHero();
    }    

    // �ʵ���� ����Ʈ�� �߰�
    public void AddFieldMonsterList(MonsterObject monsterObject)
    {
        _fieldMonsterList.Add(monsterObject);
    }

    // �ʵ���� ����Ʈ���� ����
    public void RemoveFieldMonsterList(MonsterObject monsterObject)
    {
        _fieldMonsterList.Remove(monsterObject);
    }

    // ���� �Ÿ� �� ������ ����Ʈ�� �޾ƿ�
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

    // ���� �Ÿ� �� ���� ����Ʈ �߿���, �÷��̾�� ���� ����� ���� 1������ ��ȯ
    public MonsterObject GetClosestMonstersByDistance(Vector3 pos, float distance)
    {
        // ��Ÿ� �� ���� ��� ����Ʈ�� ����
        List<MonsterObject> closeMonsterList = new List<MonsterObject>();

        for (int i = 0; i < _fieldMonsterList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldMonsterList[i].transform.position))
            {
                closeMonsterList.Add(_fieldMonsterList[i]);
            }
        }

        // �� ����Ʈ���� ���� ����� ���� ��ȯ
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


    // �ʵ���͵� ���� ���� ����ٴϵ��� 
    private void AllFieldMonsterFollowHero()
    {
        foreach (MonsterObject monster in _fieldMonsterList)
            monster.FollowHero();
    }
}
