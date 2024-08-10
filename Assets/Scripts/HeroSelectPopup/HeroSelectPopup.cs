using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeroSelectPopup : SerializedMonoBehaviour
{
    // 히어로 슬롯들 배열
    [SerializeField]
    private HeroSlot[] _heroSlotArr;

    private void Start()
    {
        HeroSlotsInit();
    }

    // 히어로 슬롯들 초기화 (슬롯들에게 이닛하라고 전달)
    private void HeroSlotsInit()
    {
        Dictionary<HeroID, HeroData> heroDataDict = PlaySceneManager.Instance.HeroDataManager.HeroDataDict;

        for (int i = 0; i < _heroSlotArr.Length; i++)
        {
            _heroSlotArr[i].InitInfoUI(heroDataDict[(HeroID)i], ClosePopup);
        }
    }

    // 팝업 닫기
    public void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}
