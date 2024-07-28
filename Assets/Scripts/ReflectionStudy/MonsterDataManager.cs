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


public class MonsterDataManager
{
    public static Dictionary<string, MonsterData> _monsterDataDict = new Dictionary<string, MonsterData>();
}
