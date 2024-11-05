using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ų �κ��丮 Ŭ���� : ������ �ִ� ��ų ����Ʈ / ����Ʈ�� ��ų ���ϱ� / ����Ʈ�� ��ų ����
/// </summary>
public class SkillManager : SerializedMonoBehaviour
{
    [HideInInspector]
    public Action<List<Skill_Base>> OnRefreshHaveSkillUI;                           // ������ �ִ� ��ų UI ���� �̺�Ʈ                    

    public List<Skill_Base> HaveSkillList { get; set; } = new List<Skill_Base>();   // ������ �ִ� ��ų��      


    /// <summary>
    /// ��ųID�� Level�� ��ų������ �������� �Լ�
    /// </summary>
    public SkillData GetSkillData_by_SkillIDLevel(SkillID skillID, int level)
    {
        // ��ųID�� �ش��ϴ� ��ų �����͸� ��� ����Ʈ ��������
        List<SkillData> SkillDataList = DataManager.Instance.SkillDatas.GetAllDataByCondition(
                skillData => skillData.ID.Contains(skillID.ToString()));

        // ����Ʈ���� ���ϴ� ������ ��ų ������ ��������
        SkillData SkillData = SkillDataList.Find(x => x.Level == level);

        // �� ��ų ������ ��ȯ
        return SkillData;
    }


    /// <summary>
    /// ������ ��ųID�� ��ȯ���ִ� �Լ�
    /// </summary>
    public SkillID RandomSkillID()
    {
        SkillID[] skillIDs = (SkillID[])Enum.GetValues(typeof(SkillID));
        int randomIndex = UnityEngine.Random.Range(0, skillIDs.Length);

        SkillID randomSkillID = skillIDs[randomIndex];
        return randomSkillID;
    }

    /// <summary>
    /// ��ų �κ��丮�� ��ų �߰����ִ� �Լ�
    /// </summary>
    public void AddSkill(SkillID newSkillID)
    {
        // ����Ʈ���� �̹� ������ �ִ� ��ų���� Ȯ��
        Skill_Base foundSkill = HaveSkillList.Find(skill => skill.Id == newSkillID);

        // �̹� ������ �ִ� ��ų�̶�� ������
        if (foundSkill != null)
        {
            foundSkill.LevelUp();
        }
        // ������ ���� �ʴٸ� ���� �߰�
        else
        {
            Skill_Base skill = SkillFactory.CreateSkillClass(newSkillID, 1);
            HaveSkillList.Add(skill);
        }


        // UI ����
        PlaySceneCanvas.Instance.SkillInvenUI.SlotRefresh(HaveSkillList);        
    }

    /// <summary>
    /// ��ų ���� ���� Ȯ���ϴ� �Լ�
    /// </summary>
    public bool HasSkill(SkillID skillID)
    {
        // ����Ʈ���� �̹� ������ �ִ� ��ų���� Ȯ���ؼ� bool ��ȯ
        return HaveSkillList.Exists(x => x.Id == skillID);
    }

    /// <summary>
    /// ��ų ���� �������� �Լ�
    /// </summary>
    public int GetSkillLevel(SkillID skillID)
    {
        // ����Ʈ���� �̹� ������ �ִ� ��ų���� Ȯ���ؼ� ���� ��ȯ
        Skill_Base foundSkill = HaveSkillList.Find(x => x.Id == skillID);

        return foundSkill != null ? foundSkill.CurrentLevel : 1;
    }

    /// <summary>
    /// ��ų ������ �������� �Լ�
    /// </summary>
    public Sprite GetSkillIcon(SkillID skillID)
    {
        Skill_Base foundSkill = HaveSkillList.Find(x => x.Id == skillID);

        if (foundSkill == null)
        {
            Debug.Log("GetSkillIcon Failed!");
            return null;
        }

        Sprite icon = ResourceManager.Instance.SkillIconDict[skillID];
        return icon;
    }

    /// <summary>
    /// ��ų �̸� �������� �Լ�
    /// </summary>
    public string GetSkillName(SkillID skillID)
    {
        Skill_Base foundSkill = HaveSkillList.Find(x => x.Id == skillID);

        if (foundSkill == null)
        {
            Debug.Log("GetSkillName Failed!");
            return null;
        }

        SkillData skillData = GetSkillData_by_SkillIDLevel(skillID, GetSkillLevel(skillID));
        return skillData.Name;
    }

    /// <summary>
    /// ��ų ���� �������� �Լ�
    /// </summary>
    public string GetSkillDesc(SkillID skillID)
    {
        Skill_Base foundSkill = HaveSkillList.Find(x => x.Id == skillID);

        if (foundSkill == null)
        {
            Debug.Log("GetSkillDesc Failed!");
            return null;
        }

        SkillData skillData = GetSkillData_by_SkillIDLevel(skillID, GetSkillLevel(skillID));
        return skillData.Desc;
    }
}
