using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataManager : SerializedMonoBehaviour
{
    // 프로젝트의 몬스터 ScriptableObject를 모두 받아와서 저장할 딕셔너리
    public Dictionary<MonsterID, MonsterData> _monsterDataDict = new Dictionary<MonsterID, MonsterData>();
}
