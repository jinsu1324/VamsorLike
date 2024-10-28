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
    /// ��ų ������ ��ųʸ����� -> ���ϴ� ��ų + ���� �� ����ȯ�Ͽ� ��ȯ���ִ� �Լ�
    /// </summary>
    public SkillData SkillData_as_Dict<SkillData>(SkillID skillID, int skillLevelNum) where SkillData : SkillData_Base
    {
        SkillData skillData = DataManager.Instance.SkillDataDict[skillID].SkillDataList[skillLevelNum] as SkillData;
        return skillData;
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
            Skill_Base skill = SkillFactory.CreateSkillClass(newSkillID, 0);
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

        return foundSkill != null ? foundSkill.CurrentLevel : 0;
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
        else
        {
            Sprite icon = DataManager.Instance.SkillDataDict[skillID].SkillDataList[foundSkill.CurrentLevel].Icon;
            return icon;
        }
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
        else
        {
            string name = DataManager.Instance.SkillDataDict[skillID].SkillDataList[foundSkill.CurrentLevel].Name;
            return name;
        }
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
        else
        {
            string desc = DataManager.Instance.SkillDataDict[skillID].SkillDataList[foundSkill.CurrentLevel].Desc;
            return desc;
        }
    }
}
