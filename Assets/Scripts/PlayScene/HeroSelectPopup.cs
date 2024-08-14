using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeroSelectPopup : SerializedMonoBehaviour
{
    // ¿µ¿õ ½½·Ôµé ¹è¿­
    [SerializeField]
    private HeroSlot[] _heroSlotArr;

    private void Start()
    {
        HeroSlotsInit();
    }

    // ÆË¾÷ ´Ý±â
    public void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }

    // ¿µ¿õ ½½·Ôµé ÃÊ±âÈ­ (½½·Ôµé¿¡°Ô ÀÌ´ÖÇÏ¶ó°í Àü´Þ)
    private void HeroSlotsInit()
    {
        Dictionary<HeroID, HeroData> heroDataDict = Managers.Instance.DataManager.HeroDataDict;

        for (int i = 0; i < _heroSlotArr.Length; i++)
        {
            _heroSlotArr[i].InitInfoUI(heroDataDict[(HeroID)i], ClosePopup);
        }
    }

    
}
