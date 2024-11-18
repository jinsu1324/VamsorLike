using DarkPixelRPGUI.Scripts.UI.Equipment;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectPopup_Slot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;              // 스킬 이름 텍스트

    [SerializeField]
    private TextMeshProUGUI _levelText;             // 스킬 레벨 텍스트

    [SerializeField]
    private TextMeshProUGUI _descText;              // 스킬 설명 텍스트

    [SerializeField]
    private Image _iconImage;                       // 스킬 아이콘 이미지

    [SerializeField]
    private Button _slotButton;                     // 스킬 슬롯 버튼

    private SkillManager _skillManager;             // 스킬 매니저 담을 변수
    private SkillSelectPopup _skillSelectPopup;     // 스킬 선택 팝업 담을 변수

    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    public void Initialize(SkillID skillID, SkillSelectPopup skillSelectPopup)
    {
        // 필요한 변수등 할당
        _skillManager = PlaySceneManager.Instance.SkillManager;
        _skillSelectPopup = skillSelectPopup;

        // 해당스킬 레벨과 다음레벨 계산
        int skillLevel = _skillManager.GetSkillLevel(skillID);
        int nextLevel = skillLevel + 1;

        // 그 값에 따라 스킬데이터 가져오기
        SkillData skillData = _skillManager.GetSkillData_by_SkillIDLevel(skillID, nextLevel);

        // UI에 표시
        _iconImage.sprite = ResourceManager.Instance.SkillIconDict[skillID];
        _nameText.text = skillData.Name;
        _descText.text = skillData.Desc;
        if (nextLevel == 1)
        {
            _levelText.text = "신규!";
        }
        else
        {
            _levelText.text = $"Lv.{nextLevel.ToString()}";
        }

        // 버튼에 이벤트 등록 및 중복 이벤트 방지
        _slotButton.onClick.RemoveAllListeners();
        _slotButton.onClick.AddListener(() => OnSkillSelected(skillID));

        // 슬롯 켜기
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 스킬슬롯 선택되었을때 실행될 함수
    /// </summary>
    private void OnSkillSelected(SkillID skillID)
    {
        // 스킬 추가
        _skillManager.AddSkill(skillID);

        // 게임 시작
        bool isGameStart = PlaySceneManager.Instance.IsGameStart;
        if (isGameStart == false)
            PlaySceneManager.Instance.IsGameStartChange(true);

        // 스킬선택 팝업 닫기
        _skillSelectPopup.ClosePopup();

        // 오디오 재생
        AudioManager.Instance.PlaySFX(SFXType.ButtonClick);
    }

    /// <summary>
    /// 슬롯 닫기
    /// </summary>
    public void HideSlot()
    {
        gameObject.SetActive(false);
    }
}
