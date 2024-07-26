using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum monsterStat
{
    NAME,
    HP,
    ATK,
    SPEED
}


public class MonsterInfo
{
    public string NAME;
    public int HP;
    public int ATK;
    public float SPEED;

    public static Dictionary<string, MonsterInfo> monsterInfos = new Dictionary<string, MonsterInfo>();

    public override string ToString()
    {
        return $"NAME : {NAME} / HP : {HP} / ATK : {ATK} / SPEED : {SPEED}";
    }
}
