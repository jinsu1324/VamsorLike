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
    public static List<Skill_Base> PlayerSkillsList { get; set; } = new List<Skill_Base>();

    /// <summary>
    /// ��ų �κ��丮�� ��ų �߰����ִ� �Լ�
    /// </summary>
    public void AddSkill(SkillID newSkillID)
    {
        // ����Ʈ���� �̹� ������ �ִ� ��ų���� Ȯ��
        Skill_Base foundSkill = PlayerSkillsList.Find(skill => skill.Id == newSkillID);

        // �̹� ������ �ִ� ��ų�̶�� ������
        if (foundSkill != null)
        {
            foundSkill.LevelUp();
        }
        // ������ ���� �ʴٸ� ���� �߰�
        else
        {
            Skill_Base skill = SkillFactory.CreateSkillClass(newSkillID, 1);
            PlayerSkillsList.Add(skill);
        }
    }

    /// <summary>
    /// ��ų ���� ���� Ȯ���ϴ� �Լ�
    /// </summary>
    public bool HasSkill(SkillID skillID)
    {
        // ����Ʈ���� �̹� ������ �ִ� ��ų���� Ȯ���ؼ� bool ��ȯ
        return PlayerSkillsList.Exists(x => x.Id == skillID);
    }

    /// <summary>
    /// ��ų ���� �������� �Լ�
    /// </summary>
    public int GetSkillLevel(SkillID skillID)
    {
        // ����Ʈ���� �̹� ������ �ִ� ��ų���� Ȯ���ؼ� ���� ��ȯ
        Skill_Base foundSkill = PlayerSkillsList.Find(x => x.Id == skillID);
        return foundSkill != null ? foundSkill.CurrentLevel : 0;
    }
}
