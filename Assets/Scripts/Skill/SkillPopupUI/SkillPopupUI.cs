using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스킬 선택 팝업 UI
/// </summary>
public class SkillPopupUI : MonoBehaviour
{
    // 스킬 버튼 프리팹
    [SerializeField]
    private GameObject _skillButtonPrefab;
    
    // 버튼 부모
    [SerializeField]
    private Transform _buttonParent;

    // 플레이어 스킬 인벤토리가 있는 스킬매니저
    private PlayerSkillManager _playerSkillManager = new PlayerSkillManager();

    // 사용 가능한 스킬 리스트
    private List<Skill_Base> _availableSkillList = new List<Skill_Base>()
    {
        new Skill_SlashAttack(),
        new Skill_Boomerang(),
        new Skill_Sniper()
    };

    //private void Awake()
    //{
    //    _availableSkillList.Add(new Skill_SlashAttack(DataManager.Instance.SkillData_as_SkillDataDict<SkillData_SlashAttack>(SkillID.SlashAttack, 0)));
    //    _availableSkillList.Add(new Skill_Boomerang(DataManager.Instance.SkillData_as_SkillDataDict<SkillData_Boomerang>(SkillID.Boomerang, 0)));
    //    _availableSkillList.Add(new Skill_Sniper(DataManager.Instance.SkillData_as_SkillDataDict<SkillData_Sniper>(SkillID.Sniper, 0)));

    //}


    /// <summary>
    /// 팝업 열기
    /// </summary>
    public void OpenSkillPopup()
    {
        // 자식 버튼들 삭제
        foreach (Transform child in _buttonParent)
        {
            Destroy(child.gameObject);
        }

        // 사용 가능한 스킬들을 버튼으로 생성
        foreach (Skill_Base skill in _availableSkillList)
        {
            GameObject skillButtonPrefab = Instantiate(_skillButtonPrefab, _buttonParent);
            Button skillButton = skillButtonPrefab.GetComponent<Button>();
            TextMeshProUGUI skillText = skillButtonPrefab.GetComponentInChildren<TextMeshProUGUI>();

            // 이미 스킬을 가지고 있다면 레벨 표시
            if (_playerSkillManager.HasSkill(skill))
            {
                int currentLevel = _playerSkillManager.GetSkillLevel(skill);
                skillText.text = $"{skill.Name} (Level {currentLevel + 1})";
            }
            // 가지고 있지 않다면 스킬 이름만 표시
            else
            {
                skillText.text = skill.Name;
            }

            // 버튼 클릭 시 스킬 선택
            skillButton.onClick.AddListener(() => OnSkillSelected(skill));

            // 팝업 켜기
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 스킬 선택 시 호출되는 함수
    /// </summary>
    public void OnSkillSelected(Skill_Base skill)
    {
        _playerSkillManager.AddSkill(skill);

        // 게임시작 안되어있었다면
        if (PlaySceneManager.Instance.IsGameStart == false)
        {
            // 게임시작을 true로
            PlaySceneManager.Instance.IsGameStartChange(true);

            // 몬스터 스폰 시작
            MonsterSpawner.Instance.StartMonsterSpawn();
        }

        CloseSkillPopup();
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void CloseSkillPopup()
    {
        gameObject.SetActive(false);
    }
}
