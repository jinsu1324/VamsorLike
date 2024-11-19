using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스킬 인벤토리 클래스 : 가지고 있는 스킬 리스트 / 리스트에 스킬 더하기 / 리스트에 스킬 빼기
/// </summary>
public class SkillManager : SerializedMonoBehaviour
{
    // 가지고 있는 스킬들  
    public List<Skill_Base> HaveSkillList { get; set; } = new List<Skill_Base>();

    // 겹치지 않는 랜덤한 스킬ID를 반환에 사용할 스킬ID들 리스트
    private List<SkillID> _remainSkillIDList;
    
    // 남은 스킬 개수를 반환하는 프로퍼티
    public int RemainSkillCount => _remainSkillIDList.Count;

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        ResetRemainSkillIDList();
    }

    /// <summary>
    /// 스킬ID와 Level로 스킬데이터 가져오는 함수
    /// </summary>
    public SkillData GetSkillData_by_SkillIDLevel(SkillID skillID, int level)
    {
        // 스킬ID에 해당하는 스킬 데이터를 모두 리스트 가져오기
        List<SkillData> SkillDataList = DataManager.Instance.SkillDatas.GetAllDataByCondition(
                skillData => skillData.ID.Contains(skillID.ToString()));

        // 리스트에서 원하는 레벨의 스킬 데이터 가져오기
        SkillData SkillData = SkillDataList.Find(x => x.Level == level);

        // 그 스킬 데이터 반환
        return SkillData;
    }

    /// <summary>
    /// 스킬 인벤토리에 스킬 추가해주는 함수
    /// </summary>
    public void AddSkill(SkillID newSkillID)
    {
        // 리스트에서 이미 가지고 있는 스킬인지 확인
        Skill_Base foundSkill = HaveSkillList.Find(skill => skill.ID == newSkillID);

        // 이미 가지고 있는 스킬이라면 레벨업
        if (foundSkill != null)
        {
            foundSkill.LevelUp();
        }
        // 가지고 있지 않다면 새로 추가
        else
        {
            Skill_Base skill = SkillFactory.CreateSkillClass(newSkillID, 1);
            HaveSkillList.Add(skill);
        }


        // UI 갱신
        PlaySceneCanvas.Instance.SkillInvenUI.SlotRefresh(HaveSkillList);        
    }

    /// <summary>
    /// 스킬 보유 여부 확인하는 함수
    /// </summary>
    public bool HasSkill(SkillID skillID)
    {
        // 리스트에서 이미 가지고 있는 스킬인지 확인해서 bool 반환
        return HaveSkillList.Exists(x => x.ID == skillID);
    }

    /// <summary>
    /// 스킬 레벨 가져오는 함수
    /// </summary>
    public int GetSkillLevel(SkillID skillID)
    {
        // 리스트에서 이미 가지고 있는 스킬인지 확인해서 레벨 반환
        Skill_Base foundSkill = HaveSkillList.Find(x => x.ID == skillID);

        return foundSkill != null ? foundSkill.CurrentLevel : 0;
    }

    /// <summary>
    /// 스킬의 맥스레벨 가져오는 함수
    /// </summary>
    public int GetSkillMaxLevel(SkillID skillID)
    {
        // 스킬ID에 해당하는 스킬 데이터를 모두 리스트 가져오기
        List<SkillData> SkillDataList = DataManager.Instance.SkillDatas.GetAllDataByCondition(
                skillData => skillData.ID.Contains(skillID.ToString()));

        // 레벨 오름차순 정렬
        SkillDataList = SkillDataList.OrderBy(skillData => skillData.Level).ToList();

        // 가장 마지막 레벨을 최대레벨 변수에 담음
        int count = SkillDataList.Count;
        int maxLevel = SkillDataList[count - 1].Level;

        return maxLevel;
    }

    /// <summary>
    /// 가지고 있는 스킬이 맥스레벨인지 확인해주는 함수
    /// </summary>
    public bool IsSkillMaxLevel(SkillID skillID)
    {
        // 맥스레벨 알아옴   
        int maxLevel = GetSkillMaxLevel(skillID);

        // 현재레벨 알아옴
        int skillLevel = GetSkillLevel(skillID);

        // true false 리턴
        return skillLevel >= maxLevel;
    }

    /// <summary>
    /// 스킬 아이콘 가져오는 함수
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
    /// 스킬 이름 가져오는 함수
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
    /// 스킬 설명 가져오는 함수
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
    /// 랜덤한 스킬ID를 반환해주는 함수
    /// </summary>
    public SkillID RandomSkillID()
    {
        SkillID[] skillIDs = (SkillID[])Enum.GetValues(typeof(SkillID));
        int randomIndex = UnityEngine.Random.Range(0, skillIDs.Length);

        SkillID randomSkillID = skillIDs[randomIndex];
        return randomSkillID;
    }

    /// <summary>
    /// 겹치지 않는 랜덤한 스킬ID를 반환해주는 함수
    /// </summary>
    public SkillID RandomUniqueSkillID()
    {
        // 랜덤 인덱스 선택
        int randomIndex = UnityEngine.Random.Range(0, _remainSkillIDList.Count);

        // 선택된 SkillID를 저장하고, 리스트에서 제거하여 중복 방지
        SkillID randomSkillID = _remainSkillIDList[randomIndex];
        _remainSkillIDList.RemoveAt(randomIndex);

        return randomSkillID;
    }

    /// <summary>
    /// 전체 SkillID 리스트를 다시 리셋
    /// </summary>
    public void ResetRemainSkillIDList()
    {
        //Debug.Log("전체 SkillID 리스트 리셋");

        _remainSkillIDList = new List<SkillID>();
        
        foreach (SkillID skillID in (SkillID[])Enum.GetValues(typeof(SkillID)))
        {
            bool isSkillMaxLevel = IsSkillMaxLevel(skillID);

            // 리셋 중 maxLevel에 도달한 스킬을 제외한 것들만 다시 리스트에 담음 
            if (isSkillMaxLevel == false)
            {
                _remainSkillIDList.Add(skillID);
            }
        }
    }
}
