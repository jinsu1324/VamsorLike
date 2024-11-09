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
    private List<SkillSelectPopup_Slot> _slotList;

   

    public void InitSlotList()
    {
        foreach (SkillSelectPopup_Slot slot in _slotList) 
        {

            // 슬롯 초기화중 슬롯을 채우다가 남은 스킬 ID가 없으면 슬롯 초기화를 건너뜀
            if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
            {
                Debug.LogWarning("모든 스킬이 선택되어 더 이상 슬롯을 초기화할 수 없습니다.");
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
        // 슬롯 1개 채울 남은스킬도 없으면 팝업을 열지 않음
        if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
        {
            Debug.LogWarning("모든 스킬을 획득하여 팝업을 생략합니다.");
            // 다른 보상 해주면 될듯?
            // 주석은 내일 달자...
            return;
        }


        PlaySceneManager.Instance.IsGameStartChange(false);

        InitSlotList();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void ClosePopup()
    {
        // 랜덤 스킬ID 선택지 다시 초기화
        PlaySceneManager.Instance.SkillManager.ResetRemainSkillIDList();

        gameObject.SetActive(false);
    }
}
