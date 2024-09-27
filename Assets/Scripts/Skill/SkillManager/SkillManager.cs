using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스킬 인벤토리 클래스 : 가지고 있는 스킬 리스트 / 리스트에 스킬 더하기 / 리스트에 스킬 빼기
/// </summary>
public class SkillManager : SerializedMonoBehaviour
{
    #region 싱글톤
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
    // 스킬 데이터 딕셔너리
    public Dictionary<SkillID, SkillLevelDataSO> SkillDataDict { get; set; } = new Dictionary<SkillID, SkillLevelDataSO>();

    // 가지고 있는 스킬들
    public List<Skill_Base> HaveSkillList { get; set; } = new List<Skill_Base>();

    /// <summary>
    /// 스킬 데이터 딕셔너리에서 -> 원하는 스킬 + 레벨 로 형변환하여 반환해주는 함수
    /// </summary>
    public SkillData SkillData_as_Dict<SkillData>(SkillID skillID, int skillLevelNum) where SkillData : SkillData_Base
    {
        SkillData skillData = SkillDataDict[skillID].SkillDataList[skillLevelNum] as SkillData;
        return skillData;
    }

    /// <summary>
    /// 스킬 인벤토리에 스킬 추가해주는 함수
    /// </summary>
    public void AddSkill(SkillID newSkillID)
    {
        // 리스트에서 이미 가지고 있는 스킬인지 확인
        Skill_Base foundSkill = HaveSkillList.Find(skill => skill.Id == newSkillID);

        // 이미 가지고 있는 스킬이라면 레벨업
        if (foundSkill != null)
        {
            foundSkill.LevelUp();
        }
        // 가지고 있지 않다면 새로 추가
        else
        {
            Skill_Base skill = SkillFactory.CreateSkillClass(newSkillID, 0);
            HaveSkillList.Add(skill);
        }
    }

    /// <summary>
    /// 스킬 보유 여부 확인하는 함수
    /// </summary>
    public bool HasSkill(SkillID skillID)
    {
        // 리스트에서 이미 가지고 있는 스킬인지 확인해서 bool 반환
        return HaveSkillList.Exists(x => x.Id == skillID);
    }

    /// <summary>
    /// 스킬 레벨 가져오는 함수
    /// </summary>
    public int GetSkillLevel(SkillID skillID)
    {
        // 리스트에서 이미 가지고 있는 스킬인지 확인해서 레벨 반환
        Skill_Base foundSkill = HaveSkillList.Find(x => x.Id == skillID);

        return foundSkill != null ? foundSkill.CurrentLevel : 0;
    }


    
}
