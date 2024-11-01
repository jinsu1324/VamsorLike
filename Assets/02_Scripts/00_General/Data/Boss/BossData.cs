using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossID
{
    FireDragon
}

[System.Serializable]
public class BossData : BaseData
{
    public string Name;
    public float MaxHp;
    public float Atk;
    public float Speed;
    public int AppearStageLevel;
    public float SkillDamage; 
    public float SkillRadius; 
    public float SkillDuration; 
    public float SkillRangeMin; 
    public float SkillRangeMax; 
    public float SkillCount;
    public float SkillCoolTime;
}
