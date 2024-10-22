using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SerializedMonoBehaviour
{
    #region �̱���_���̵�x
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
        
    // �ʵ忡 �����Ǿ��ִ� ���� ����Ʈ
    private List<MonsterObject> _fieldMonsterList = new List<MonsterObject>();

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        MonsterObject.OnMonsterDeath += RemoveFieldMonsterList;
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
    /// �ʵ���� ����Ʈ�� �߰�
    /// </summary>
    public void AddFieldMonsterList(MonsterObject monsterObject)
    {
        _fieldMonsterList.Add(monsterObject);
    }

    /// <summary>
    /// �ʵ���� ����Ʈ���� ����
    /// </summary>
    public void RemoveFieldMonsterList(MonsterObject monsterObject)
    {
        _fieldMonsterList.Remove(monsterObject);
    }

    /// <summary>
    /// ���� �Ÿ� �� ������ ����Ʈ�� �޾ƿ�
    /// </summary>
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

    /// <summary>
    /// ���� �Ÿ� �� ���� ����Ʈ �߿���, �÷��̾�� ���� ����� ���� 1������ ��ȯ
    /// </summary>
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

    /// <summary>
    /// �ʵ���͵� ���� ���� ����ٴϵ��� 
    /// </summary>
    private void FollowHero_AllMonster()
    {
        foreach (MonsterObject monster in _fieldMonsterList)
            monster.FollowHero();
    }

    /// <summary>
    /// �ʵ���͵� ���� ���ߵ���
    /// </summary>
    private void Stop_AllMonster()
    {
        foreach (MonsterObject monster in _fieldMonsterList)
            monster.StopFollow();
    }
}
