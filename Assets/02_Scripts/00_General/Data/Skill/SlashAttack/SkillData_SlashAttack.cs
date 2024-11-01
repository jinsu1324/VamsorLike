using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillData_SlashAttackID
{
    Level0,
    Level1,
    Level2
}

[System.Serializable]
public class SkillData_SlashAttack : BaseData
{
    public int Level;
    public string Name;
    public string Desc;
    public float AtkPercentage;
    public float Delay;
    public ProjectileBase Projectile;
    public Sprite Icon;
}
