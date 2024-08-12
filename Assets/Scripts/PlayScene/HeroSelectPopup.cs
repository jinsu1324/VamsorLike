using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeroSelectPopup : SerializedMonoBehaviour
{
    // ���� ���Ե� �迭
    [SerializeField]
    private HeroSlot[] _heroSlotArr;

    private void Start()
    {
        HeroSlotsInit();
    }

    // ���� ���Ե� �ʱ�ȭ (���Ե鿡�� �̴��϶�� ����)
    private void HeroSlotsInit()
    {
        Dictionary<HeroID, HeroData> heroDataDict = PlaySceneManager.Instance.HeroDataManager.HeroDataDict;

        for (int i = 0; i < _heroSlotArr.Length; i++)
        {
            _heroSlotArr[i].InitInfoUI(heroDataDict[(HeroID)i], ClosePopup);
        }
    }

    // �˾� �ݱ�
    public void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}
