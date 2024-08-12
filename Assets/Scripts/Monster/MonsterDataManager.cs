using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataManager : SerializedMonoBehaviour
{
    // ������Ʈ�� ���� ScriptableObject�� ��� �޾ƿͼ� ������ ��ųʸ�
    [SerializeField]
    private Dictionary<MonsterID, MonsterData> _monsterDataDict = new Dictionary<MonsterID, MonsterData>();    
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get { return _monsterDataDict; } set { _monsterDataDict = value; } }

    // ���� �����յ� ��ųʸ�
    [SerializeField]
    private Dictionary<MonsterID, MonsterObject> _monsterObjectDict = new Dictionary<MonsterID, MonsterObject>();
    public Dictionary<MonsterID, MonsterObject> MonsterObjectDict { get { return _monsterObjectDict; } set { _monsterObjectDict = value; } }
}
