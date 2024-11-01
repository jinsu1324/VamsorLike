using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillID
{
    SlashAttack,
    Sniper,
    Boomerang
}

// 영웅 스킬 데이터
[System.Serializable]
public class SkillData_Base
{
    public string Id;
    public int Level;
    public string Name;
    public string Desc;

    public Sprite Icon;
}
