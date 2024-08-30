using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 영웅 선택 팝업 : 영웅 슬롯들 / 각 영웅슬롯들 데이터초기화 명령 / 팝업 닫기
public class HeroSelectPopup : SerializedMonoBehaviour
{
    // 영웅 슬롯들
    [SerializeField]
    private HeroSlot[] _heroSlotArr;

    private void Start()
    {
        HeroSlotsSetting();
    }   
    
    // 각각 영웅슬롯 속 ui정보들 데이터 셋팅 (슬롯들에게 셋팅하라고 전달)
    private void HeroSlotsSetting()
    {
        Dictionary<HEROID, HeroData> heroDataDict = DataManager.Instance.HeroDataDict;

        for (int i = 0; i < _heroSlotArr.Length; i++)
        {
            // 영웅 선택 완료 상황일때, 호출할 ClosePopup도 함께 인자로 넘겨주기 
            _heroSlotArr[i].UIInfoSetting(heroDataDict[(HEROID)i], ClosePopup);
        }
    }

    // 팝업 닫기
    private void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}
