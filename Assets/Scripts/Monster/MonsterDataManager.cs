using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MonsterKey
{
    Golem,
    Skeleton,
    Witch,
    Dragon
}


public class MonsterDataManager : MonoBehaviour
{
    public List<MonsterData> _monsterDataDict =
        new List<MonsterData>();
}
