using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ų �κ��丮 Ŭ���� : ������ �ִ� ��ų ����Ʈ / ����Ʈ�� ��ų ���ϱ� / ����Ʈ�� ��ų ����
/// </summary>
public class SkillManager : SerializedMonoBehaviour
{
    #region �̱���
    private static SkillManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion

    [SerializeField]
    // ��ų ������ ��ųʸ�
    public Dictionary<SkillID, SkillLevelDataSO> SkillDataDict { get; set; } = new Dictionary<SkillID, SkillLevelDataSO>();

    // ������ �ִ� ��ų��
    public List<Skill_Base> HaveSkillList { get; set; } = new List<Skill_Base>();

    /// <summary>
    /// ��ų ������ ��ųʸ����� -> ���ϴ� ��ų + ���� �� ����ȯ�Ͽ� ��ȯ���ִ� �Լ�
    /// </summary>
    public SkillData SkillData_as_Dict<SkillData>(SkillID skillID, int skillLevelNum) where SkillData : SkillData_Base
    {
        SkillData skillData = SkillDataDict[skillID].SkillDataList[skillLevelNum] as SkillData;
        return skillData;
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


    
}
