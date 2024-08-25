using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillID
{
    SlashAttack,
    Sniper,
    Boomerang,
    Heal
}

// 영웅 스킬 데이터
public class SkillData : ScriptableObject
{
    public string Id;
    public string Name;
    public int Atk;
    public int HealPoint;
    public float ProjectileSpeed;
    public float Range;
    public float Delay;
    public string Desc;

    public ProjectileBase Projectile;
}
