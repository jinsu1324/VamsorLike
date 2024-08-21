using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 영웅이 장착하고있는 스킬들 : 장착중인 스킬 리스트 / 리스트에 스킬 더하기 / 리스트에 스킬 빼기
public class HeroEquipedSkill : SerializedMonoBehaviour
{
    #region 싱글톤
    private static HeroEquipedSkill _instance;

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

    public static HeroEquipedSkill Instance
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

    // 영웅이 장착하고있는 스킬들
    public List<Skill> EquipedSkillList { get; set; } = new List<Skill>();
       

    // 장착 스킬 리스트에 스킬 추가
    public void AddSkill(SkillID choicedSkillID)
    {
        Skill choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Add(choicedSkill);
    }

    // 장착 스킬 리스트에 스킬 삭제
    public void RemoveSkill(SkillID choicedSkillID)
    {
        Skill choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Remove(choicedSkill);
    }

    // SkillID에 따라 Skill을 반환해주는 함수
    private Skill ReturnSkillByID(SkillID skillID)
    {
        // 스킬 데이터 딕셔너리
        Dictionary<SkillID, SkillData> skillDataDict = Managers.Instance.DataManager.SkillDataDict;
        // 이번게임 플레이중인 영웅
        HeroObject thisGameHeroObject = PlaySceneManager.ThisGameHeroObject;

        // 스킬ID에 따라 Skill 인스턴스화 해서 반환
        switch (skillID)
        {
            case SkillID.SlashAttack:
                SkillSlashAttack skillBoomerang = new SkillSlashAttack(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("SlashAttack 이 선택되었습니다!");
                return skillBoomerang;

            case SkillID.Sniper:
                SkillBoomerang skillBoomerang2 = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("Sniper 이 선택되었습니다!(임시로 부메랑 들어가있음)");
                return skillBoomerang2;

            case SkillID.Boomerang:
                SkillBoomerang skillBoomerang3 = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("Boomerang 이 선택되었습니다!");
                return skillBoomerang3;

            case SkillID.Heal:
                SkillBoomerang skillBoomerang4 = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("Heal 이 선택되었습니다!(임시로 부메랑 들어가있음)");
                return skillBoomerang4;
        }

        Debug.Log("ReturnSkillByID 아무것도 반환하지 못했습니다");
        return null;
    }
}
