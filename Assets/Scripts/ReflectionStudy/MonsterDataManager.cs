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

//[CreateAssetMenu(fileName = "NewMonster_", menuName = "Assets/NewMonster")]
public class MonsterInfo
{
    public string NAME;
    public int HP;
    public int ATK;
    public float SPEED;

    public override string ToString()
    {
        return $"NAME : {NAME} / HP : {HP} / ATK : {ATK} / SPEED : {SPEED}";
    }
}


public class MonsterDataManager
{
    public static Dictionary<string, MonsterInfo> _monsterInfoDict = new Dictionary<string, MonsterInfo>();
}
