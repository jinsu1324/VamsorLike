using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossID
{
    FireDragon
}

public class BossData : Creature
{
    public float AppearTime;
    public float SkillDamage; 
    public float SkillRadius; 
    public float SkillDuration; 
    public float SkillRangeMin; 
    public float SkillRangeMax; 
    public float SkillCount;
    public float SkillCoolTime;
}
