using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardBoxPopup : MonoBehaviour
{
    [SerializeField]
    private Image _skillIconImage;                  // 스킬 아이콘
    [SerializeField]
    private TextMeshProUGUI _skillNameText;         // 스킬 이름 텍스트
    [SerializeField]
    private TextMeshProUGUI _skillLevelText;        // 스킬 레벨 텍스트
    [SerializeField]
    private TextMeshProUGUI _skillDescText;         // 스킬 설명 텍스트
    [SerializeField]
    private GameObject _rewardGO;                   // 보상 게임오브젝트

    
    /// <summary>
    /// 팝업 시작 시 초기화
    /// </summary>
    public void Initialize()
    {
        // 남은 스킬이 1개도 없으면 팝업을 열지 않음
        if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
        {
            Debug.Log("모든 스킬을 획득하여 팝업을 생략합니다.");

            // 골드 보상으로 일단
            PlaySceneCanvas.Instance.PlayAchivementUI.AddGold(5);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);

            return;
        }



        PlaySceneManager.Instance.IsGameStartChange(false);

        SettingRewardSkill();

        _rewardGO.SetActive(false);

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 보상 게임오브젝트 열기
    /// </summary>
    public void OpenRewardGO()
    {
        _rewardGO.SetActive(true);

        Invoke("ClosePopup", 3.0f);
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public void ClosePopup()
    {
        _rewardGO.SetActive(false);
        gameObject.SetActive(false);

        PlaySceneManager.Instance.IsGameStartChange(true);
    }

    /// <summary>
    /// 랜덤스킬 가져오고 인벤에 추가 및 UI에 정보 셋팅하는 함수
    /// </summary>
    private void SettingRewardSkill()
    {
        SkillManager skillManager = PlaySceneManager.Instance.SkillManager;

        // 겹치지 않는 랜덤한 스킬ID를 받아옴
        SkillID skillID = skillManager.RandomUniqueSkillID();

        // 해당스킬 레벨과 다음레벨 계산
        int skillLevel = skillManager.GetSkillLevel(skillID);
        int nextLevel = skillLevel + 1;

        // 그 값에 따라 스킬데이터 가져오기
        SkillData skillData = skillManager.GetSkillData_by_SkillIDLevel(skillID, nextLevel);

        // UI에 표시
        _skillIconImage.sprite = ResourceManager.Instance.SkillIconDict[skillID];
        _skillNameText.text = skillData.Name;
        _skillDescText.text = skillData.Desc;
        if (nextLevel == 1)
        {
            _skillLevelText.text = "신규!";
        }
        else
        {
            _skillLevelText.text = $"Lv.{nextLevel.ToString()}";
        }

        // 받아온 스킬 인벤토리에 추가
        skillManager.AddSkill(skillID);

        // 스킬 풀 초기화
        skillManager.ResetRemainSkillIDList();
    }
}
