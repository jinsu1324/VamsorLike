using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPopupUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _skillButtonPrefab;
    [SerializeField]
    private Transform _buttonParent;

    private PlayerSkillManager _playerSkillManager = new PlayerSkillManager();

    private List<Skill_Base> _availableSkills = new List<Skill_Base>
    {
        new Skill_SlashAttack(),
        new Skill_Sniper(),
        new Skill_Boomerang(),
    };


    /// <summary>
    /// 팝업 열기 함수
    /// </summary>
    public void OpenSkillPopup()
    {
        gameObject.SetActive(true);

        foreach (Transform child in _buttonParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Skill_Base skill in _availableSkills)
        {
            GameObject skillButtonPrefab = Instantiate(_skillButtonPrefab, _buttonParent);
            Button skillButton = skillButtonPrefab.GetComponent<Button>();
            TextMeshProUGUI skillText = skillButtonPrefab.GetComponentInChildren<TextMeshProUGUI>();

            if (_playerSkillManager.HasSkill(skill))
            {
                int currentLevel = _playerSkillManager.GetSkillLevel(skill);
                skillText.text = $"{skill.Name} (Level {currentLevel + 1})";
            }
            else
            {
                skillText.text = skill.Name;
            }

            // 버튼 클릭 시 스킬 선택
            skillButton.onClick.AddListener(() => OnSkillSelected(skill));
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
