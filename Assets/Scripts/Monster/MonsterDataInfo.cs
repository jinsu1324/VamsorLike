using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/MonsterDataInfo")]
public class MonsterDataInfo : ScriptableObject
{
    public List<MonsterData> monsterDataList = new List<MonsterData>();
}
