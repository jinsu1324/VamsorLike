using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ų �κ��丮 Ŭ���� : ������ �ִ� ��ų ����Ʈ / ����Ʈ�� ��ų ���ϱ� / ����Ʈ�� ��ų ����
/// </summary>
public class PlayerSkillManager
{
    // ������ �ִ� ��ų��
    public static List<Skill_Base> playerSkillsList { get; set; } = new List<Skill_Base>();

    /// <summary>
    /// ��ų �κ��丮�� ��ų �߰����ִ� �Լ�
    /// </summary>
    public void AddSkill(Skill_Base newSkill)
    {
        Skill_Base foundSkill = playerSkillsList.Find(skill => skill.Id == newSkill.Id);

        if (foundSkill != null)
        {
            foundSkill.LevelUp();
        }
        else
        {
            playerSkillsList.Add(newSkill);
        }
    }

    /// <summary>
    /// ��ų ���� ���� Ȯ���ϴ� �Լ�
    /// </summary>
    public bool HasSkill(Skill_Base skill)
    {
        return playerSkillsList.Exists(x => x.Id == skill.Id);
    }

    /// <summary>
    /// ��ų ���� �������� �Լ�
    /// </summary>
    public int GetSkillLevel(Skill_Base skill)
    {
        Skill_Base foundSkill = playerSkillsList.Find(x => x.Id == skill.Id);
        return foundSkill != null ? foundSkill.CurrentLevel : 0;
    }
}
