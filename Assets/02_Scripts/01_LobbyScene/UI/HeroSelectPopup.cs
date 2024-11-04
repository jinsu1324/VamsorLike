using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 영웅 선택 팝업 : 영웅 슬롯들 / 각 영웅슬롯들 데이터초기화 명령 / 팝업 닫기
public class HeroSelectPopup : SerializedMonoBehaviour
{    
    [SerializeField]
    private HeroSelectPopup_Slot[] _heroSlotArr;                // 영웅 슬롯들

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        HeroSlotsSetting();
    }   
    
    /// <summary>
    /// 각각 영웅슬롯 속 ui정보들 데이터 셋팅 (슬롯들에게 셋팅하라고 전달)
    /// </summary>
    private void HeroSlotsSetting()
    {
        // 미사용
        //Dictionary<HeroID, HeroData> heroDataDict = DataManager.Instance.HeroDataDict;

        //HeroDatas heroDatas = DataManager.Instance.HeroDatas;


        //for (int i = 0; i < _heroSlotArr.Length; i++)
        //{
        //    // 영웅 선택 완료 상황일때, 호출할 ClosePopup도 함께 인자로 넘겨주기 
        //    _heroSlotArr[i].UIInfoSetting(heroDataDict[(HeroID)i], ClosePopup);
        //}
    }

    /// <summary>
    /// 팝업 열기
    /// </summary>
    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
