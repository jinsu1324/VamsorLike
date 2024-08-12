using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataManager : SerializedMonoBehaviour
{
    // 프로젝트의 몬스터 ScriptableObject를 모두 받아와서 저장할 딕셔너리
    [SerializeField]
    private Dictionary<MonsterID, MonsterData> _monsterDataDict = new Dictionary<MonsterID, MonsterData>();    
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get { return _monsterDataDict; } set { _monsterDataDict = value; } }

    // 몬스터 프리팹들 딕셔너리
    [SerializeField]
    private Dictionary<MonsterID, MonsterObject> _monsterObjectDict = new Dictionary<MonsterID, MonsterObject>();
    public Dictionary<MonsterID, MonsterObject> MonsterObjectDict { get { return _monsterObjectDict; } set { _monsterObjectDict = value; } }
}
