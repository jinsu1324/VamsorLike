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

    [SerializeField]
    private List<SkillSelectPopup_Slot> _slotList;              // 스킬 슬롯 리스트

    [SerializeField]
    private GameObject _firstPopParticle;                       // 처음 뜰 이펙트

    /// <summary>
    /// 스킬슬롯 리스트 초기화
    /// </summary>
    public void InitSlotList()
    {
        foreach (SkillSelectPopup_Slot slot in _slotList) 
        {

            // 슬롯 하나씩 초기화다가 남은 스킬 ID가 없으면, 슬롯 초기화를 건너뜀 (보통 남은스킬이 1~2개인경우)
            if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
            {
                Debug.Log("모든 스킬이 선택되어 더 이상 슬롯을 초기화할 수 없습니다.");
                slot.HideSlot();
                break;
            }
            else
            {
                SkillID randomSkillID = PlaySceneManager.Instance.SkillManager.RandomUniqueSkillID();
                slot.Initialize(randomSkillID, this);
            }
        }
    }

    /// <summary>
    /// 팝업 열기
    /// </summary>
    public void OpenPopup()
    {
        // 남은 스킬이 1개도 없으면 팝업을 열지 않음
        if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
        {
            Debug.Log("모든 스킬을 획득하여 팝업을 생략합니다.");
            
            // 다른 보상
            
            return;
        }

        // 게임진행 멈추기
        PlaySceneManager.Instance.IsGameStartChange(false);

        // 슬롯 초기화
        InitSlotList();

        // 팝업 켜기
        gameObject.SetActive(true);

        // 파티클 오브젝트 켜기
        _firstPopParticle.SetActive(true);
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void ClosePopup()
    {
        // 랜덤스킬ID 선택지 다시 초기화
        PlaySceneManager.Instance.SkillManager.ResetRemainSkillIDList();

        // 팝업 닫기
        gameObject.SetActive(false);
    }
}
