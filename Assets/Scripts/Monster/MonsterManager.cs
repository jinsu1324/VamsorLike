using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SerializedMonoBehaviour
{
    // public delegate List<MonsterObject> GetMonstersByLength(Vector3 pos, float distance);
    // public static event GetMonstersByLength GetMonstersByLengthEvent;

    private List<MonsterObject> _spawnMonsterList = new List<MonsterObject>();

    private void Start()
    {
        //GetMonstersByLengthEvent += 
    }

    public void SpawnMonster(MonsterObject monsterObject)
    {
        _spawnMonsterList.Add(monsterObject);
    }

    public void DieMonster(MonsterObject monsterObject)
    {
        _spawnMonsterList.Remove(monsterObject);

        
        //for (int i = 0; i < _spawnMonsterList.Count; i++)
        //{
        //    if (_spawnMonsterList[i] == monsterObject)
        //    {
        //        _spawnMonsterList.RemoveAt(i);
        //    }
        //}

    }

    public List<MonsterObject> GetMonstersByDistance(Vector3 pos, float distance)
    {
        List<MonsterObject> closeMonsterList = new List<MonsterObject>();

        for (int i = 0; i < _spawnMonsterList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _spawnMonsterList[i].transform.position))
            {
                closeMonsterList.Add(_spawnMonsterList[i]);                
            }
        }

        return closeMonsterList;
    }

    public MonsterObject GetClosestMonstersByDistance(Vector3 pos, float distance)
    {
        // ��Ÿ� �� ���� ��� ����Ʈ�� ����
        List<MonsterObject> closeMonsterList = new List<MonsterObject>();

        for (int i = 0; i < _spawnMonsterList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _spawnMonsterList[i].transform.position))
            {
                closeMonsterList.Add(_spawnMonsterList[i]);
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
}
