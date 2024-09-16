using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public struct SkillAttackArgs
{
    public Vector3 StartSkillPos;   
}


/// <summary>
/// ��� ��ų�� �θ� Ŭ����
/// </summary>
public abstract class Skill_Base
{
    protected float _time;

    public string Id;
    public string Name;
    public string Desc;
    public int CurrentLevel;
    public int MaxLevel;

    // ������
    public Skill_Base(string id, string name, string desc, int maxLevel)
    {
        Id = id;
        Name = name;
        Desc = desc;
        MaxLevel = maxLevel;
        CurrentLevel = 1;
    }

    /// <summary>
    /// ��ų ������ �Լ�
    /// </summary>
    public bool LevelUp()
    {
        if (CurrentLevel < MaxLevel)
        {
            CurrentLevel++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract bool SkillCooltime();

    public abstract void UseSkill(SkillAttackArgs skillAttackArgs);

}



