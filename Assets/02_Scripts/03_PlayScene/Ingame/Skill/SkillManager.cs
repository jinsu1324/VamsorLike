using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ų �κ��丮 Ŭ���� : ������ �ִ� ��ų ����Ʈ / ����Ʈ�� ��ų ���ϱ� / ����Ʈ�� ��ų ����
/// </summary>
public class SkillManager : SerializedMonoBehaviour
{
    // ������ �ִ� ��ų��  
    public List<Skill_Base> HaveSkillList { get; set; } = new List<Skill_Base>();

    // ��ġ�� �ʴ� ������ ��ųID�� ��ȯ�� ����� ��ųID�� ����Ʈ
    private List<SkillID> _remainSkillIDList;
    
    // ���� ��ų ������ ��ȯ�ϴ� ������Ƽ
    public int RemainSkillCount => _remainSkillIDList.Count;

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        ResetRemainSkillIDList();
    }

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
    /// ��ų �κ��丮�� ��ų �߰����ִ� �Լ�
    /// </summary>
    public void AddSkill(SkillID newSkillID)
    {
        // ����Ʈ���� �̹� ������ �ִ� ��ų���� Ȯ��
        Skill_Base foundSkill = HaveSkillList.Find(skill => skill.ID == newSkillID);

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
        return HaveSkillList.Exists(x => x.ID == skillID);
    }

    /// <summary>
    /// ��ų ���� �������� �Լ�
    /// </summary>
    public int GetSkillLevel(SkillID skillID)
    {
        // ����Ʈ���� �̹� ������ �ִ� ��ų���� Ȯ���ؼ� ���� ��ȯ
        Skill_Base foundSkill = HaveSkillList.Find(x => x.ID == skillID);

        return foundSkill != null ? foundSkill.CurrentLevel : 0;
    }

    /// <summary>
    /// ��ų�� �ƽ����� �������� �Լ�
    /// </summary>
    public int GetSkillMaxLevel(SkillID skillID)
    {
        // ��ųID�� �ش��ϴ� ��ų �����͸� ��� ����Ʈ ��������
        List<SkillData> SkillDataList = DataManager.Instance.SkillDatas.GetAllDataByCondition(
                skillData => skillData.ID.Contains(skillID.ToString()));

        // ���� �������� ����
        SkillDataList = SkillDataList.OrderBy(skillData => skillData.Level).ToList();

        // ���� ������ ������ �ִ뷹�� ������ ����
        int count = SkillDataList.Count;
        int maxLevel = SkillDataList[count - 1].Level;

        return maxLevel;
    }

    /// <summary>
    /// ������ �ִ� ��ų�� �ƽ��������� Ȯ�����ִ� �Լ�
    /// </summary>
    public bool IsSkillMaxLevel(SkillID skillID)
    {
        // �ƽ����� �˾ƿ�   
        int maxLevel = GetSkillMaxLevel(skillID);

        // ���緹�� �˾ƿ�
        int skillLevel = GetSkillLevel(skillID);

        // true false ����
        return skillLevel >= maxLevel;
    }

    /// <summary>
    /// ��ų ������ �������� �Լ�
    /// </summary>
    public Sprite GetSkillIcon(SkillID skillID)
    {
        Skill_Base foundSkill = HaveSkillList.Find(x => x.ID == skillID);

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
        Skill_Base foundSkill = HaveSkillList.Find(x => x.ID == skillID);

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
        Skill_Base foundSkill = HaveSkillList.Find(x => x.ID == skillID);

        if (foundSkill == null)
        {
            Debug.Log("GetSkillDesc Failed!");
            return null;
        }

        SkillData skillData = GetSkillData_by_SkillIDLevel(skillID, GetSkillLevel(skillID));
        return skillData.Desc;
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
    /// ��ġ�� �ʴ� ������ ��ųID�� ��ȯ���ִ� �Լ�
    /// </summary>
    public SkillID RandomUniqueSkillID()
    {
        // ���� �ε��� ����
        int randomIndex = UnityEngine.Random.Range(0, _remainSkillIDList.Count);

        // ���õ� SkillID�� �����ϰ�, ����Ʈ���� �����Ͽ� �ߺ� ����
        SkillID randomSkillID = _remainSkillIDList[randomIndex];
        _remainSkillIDList.RemoveAt(randomIndex);

        return randomSkillID;
    }

    /// <summary>
    /// ��ü SkillID ����Ʈ�� �ٽ� ����
    /// </summary>
    public void ResetRemainSkillIDList()
    {
        //Debug.Log("��ü SkillID ����Ʈ ����");

        _remainSkillIDList = new List<SkillID>();
        
        foreach (SkillID skillID in (SkillID[])Enum.GetValues(typeof(SkillID)))
        {
            bool isSkillMaxLevel = IsSkillMaxLevel(skillID);

            // ���� �� maxLevel�� ������ ��ų�� ������ �͵鸸 �ٽ� ����Ʈ�� ���� 
            if (isSkillMaxLevel == false)
            {
                _remainSkillIDList.Add(skillID);
            }
        }
    }
}
