using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroSkillID
{
    SlashAttack,
    Sniper,
    Boomerang,
    Heal
}


public class HeroSkillData : ScriptableObject
{
    public string Id;
    public string Name;
    public int Atk;
    public int HealPoint;
    public float ProjectileSpeed;
    public float Range;
    public float Delay;
    public string Desc;
}
