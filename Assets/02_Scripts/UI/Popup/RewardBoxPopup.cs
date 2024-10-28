using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardBoxPopup : MonoBehaviour
{
    private Animator _animator;                     // 애니메이터

    [SerializeField]
    private Image _skillIconImage;                  // 스킬 아이콘
    [SerializeField]
    private TextMeshProUGUI _skillNameText;         // 스킬 이름 텍스트
    [SerializeField]
    private TextMeshProUGUI _skillLevelText;        // 스킬 레벨 텍스트
    [SerializeField]
    private TextMeshProUGUI _skillDescText;         // 스킬 설명 텍스트

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        // 보상상자 얻었을 때 이벤트 등록
        RewardBoxItem.OnGetRewardBox += Initialize_Popup;
    }

    /// <summary>
    /// 팝업 시작 시 초기화
    /// </summary>
    public void Initialize_Popup()
    {
        PlaySceneManager.Instance.IsGameStartChange(false);

        _animator = GetComponent<Animator>();
        SetAndAdd_RandomRewardSkill();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 애니메이터 IsOpenReady true로 변경
    /// </summary>
    public void ReadyComplete()
    {
        _animator.SetBool("isOpenReady", true);
    }

    /// <summary>
    /// 팝업 끄기 + isOpenReady false로 변경하는 함수
    /// </summary>
    public void End_Popup()
    {
        _animator.SetBool("isOpenReady", false);
        gameObject.SetActive(false);

        PlaySceneManager.Instance.IsGameStartChange(true);
    }

    /// <summary>
    /// 랜덤스킬 가져오고 인벤에 추가 및 UI에 정보 셋팅하는 함수
    /// </summary>
    private void SetAndAdd_RandomRewardSkill()
    {
        SkillManager skillManager = SkillManager.Instance;

        // 랜덤한 스킬ID를 받아옴
        SkillID randomSkillID = skillManager.RandomSkillID();
        
        // 받아온 스킬 인벤토리에 추가
        skillManager.AddSkill(randomSkillID);
        
        // 받아온 스킬로 팝업 UI들 정보 셋팅 
        Sprite skillIcon = skillManager.GetSkillIcon(randomSkillID);
        string skillName = skillManager.GetSkillName(randomSkillID);
        int skillLevel = skillManager.GetSkillLevel(randomSkillID);
        string skillDesc = skillManager.GetSkillDesc(randomSkillID);

        _skillIconImage.sprite = skillIcon;
        _skillNameText.text = skillName;
        _skillLevelText.text = $"Level {skillLevel}";
        _skillDescText.text = skillDesc;
    }

    /// <summary>
    /// 비활성화 시 이벤트 해제 (따로 빼서 더 인지되도록 함)
    /// </summary>
    private void OnDisable()
    {
        RewardBoxItem.OnGetRewardBox -= Initialize_Popup;
    }
}
