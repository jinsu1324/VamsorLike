using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스킬 선택 팝업 UI
/// </summary>
public class SkillSelectPopup : SerializedMonoBehaviour
{
    // 스킬 버튼 프리팹
    [SerializeField]
    private GameObject _skillButtonPrefab;
    
    // 버튼 부모
    [SerializeField]
    private Transform _buttonParent;

    // 사용 가능한 스킬 리스트
    private List<SkillID> _availableSkillList = new List<SkillID>()
    {
        SkillID.SlashAttack,
        SkillID.Boomerang,
        SkillID.Sniper
    };


    /// <summary>
    /// 팝업 열기
    /// </summary>
    public void OpenPopup()
    {
        // 게임시작을 false로
        PlaySceneManager.Instance.IsGameStartChange(false);

        // 자식 버튼들 삭제
        foreach (Transform child in _buttonParent)
        {
            Destroy(child.gameObject);
        }

        // 사용 가능한 스킬들을 버튼으로 생성
        foreach (SkillID skillID in _availableSkillList)
        {
            int skillLevel = PlaySceneManager.Instance.SkillManager.GetSkillLevel(skillID);
            var skillData = PlaySceneManager.Instance.SkillManager.SkillData_as_Dict<SkillData_Base>(skillID, skillLevel);

            GameObject skillButtonPrefab = Instantiate(_skillButtonPrefab, _buttonParent);
            Button skillButton = skillButtonPrefab.GetComponent<Button>();
            TextMeshProUGUI skillText = skillButtonPrefab.GetComponentInChildren<TextMeshProUGUI>();

            // 이미 스킬을 가지고 있다면 레벨 표시
            if (PlaySceneManager.Instance.SkillManager.HasSkill(skillID))
            {
                int currentLevel = PlaySceneManager.Instance.SkillManager.GetSkillLevel(skillID);
                skillText.text = $"{skillData.Name} (Level {currentLevel + 1})";
            }
            // 가지고 있지 않다면 스킬 이름만 표시
            else
            {
                skillText.text = skillData.Name;
            }

            // 버튼 클릭 시 스킬 선택
            skillButton.onClick.AddListener(() => OnSkillSelected(skillID));

            // 팝업 켜기
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 스킬 선택 시 호출되는 함수
    /// </summary>
    public void OnSkillSelected(SkillID skillID)
    {
        PlaySceneManager.Instance.SkillManager.AddSkill(skillID);

        // 게임시작 안되어있었다면 게임시작을 true로
        if (PlaySceneManager.Instance.IsGameStart == false)
            PlaySceneManager.Instance.IsGameStartChange(true);

        ClosePopup();
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
