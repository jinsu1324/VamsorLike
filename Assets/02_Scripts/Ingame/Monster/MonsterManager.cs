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
    /// �ʵ���� ����Ʈ�� �߰�
    /// </summary>
    public void AddFieldMonsterList(MonsterObjectBase monster)
    {
        _fieldMonsterList.Add(monster);
    }

    /// <summary>
    /// �ʵ���� ����Ʈ���� ����
    /// </summary>
    public void RemoveFieldMonsterList(MonsterObjectBase monster)
    {
        _fieldMonsterList.Remove(monster);
    }

    /// <summary>
    /// ���� �Ÿ� �� ������ ����Ʈ�� �޾ƿ�
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
    /// ���� �Ÿ� �� ���� ����Ʈ �߿���, �÷��̾�� ���� ����� ���� 1������ ��ȯ
    /// </summary>
    public MonsterObjectBase GetClosestMonstersByDistance(Vector3 pos, float distance)
    {
        // ��Ÿ� �� ���� ��� ����Ʈ�� ����
        List<MonsterObjectBase> closeMonsterList = new List<MonsterObjectBase>();

        for (int i = 0; i < _fieldMonsterList.Count; i++)
        {
            if (distance >= Vector3.Distance(pos, _fieldMonsterList[i].transform.position))
            {
                closeMonsterList.Add(_fieldMonsterList[i]);
            }
        }

        // �� ����Ʈ���� ���� ����� ���� ��ȯ
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
    /// �ʵ���͵� ���� ���� ����ٴϵ��� 
    /// </summary>
    private void FollowHero_AllMonster()
    {
        foreach (MonsterObjectBase monster in _fieldMonsterList)
            monster.FollowHero();
    }

    /// <summary>
    /// �ʵ���͵� ���� ���ߵ���
    /// </summary>
    private void Stop_AllMonster()
    {
        foreach (MonsterObjectBase monster in _fieldMonsterList)
            monster.StopFollow();
    }

    /// <summary>
    /// �� ��ȯ�ǰų� ������Ʈ �ı��� �� �̺�Ʈ ����
    /// </summary>
    public void OnDisable()
    {
        MonsterObjectBase.OnMonsterDeath -= RemoveFieldMonsterList;
    }
}
