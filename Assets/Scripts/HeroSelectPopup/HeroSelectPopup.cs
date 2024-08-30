using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� ���� �˾� : ���� ���Ե� / �� �������Ե� �������ʱ�ȭ ��� / �˾� �ݱ�
public class HeroSelectPopup : SerializedMonoBehaviour
{
    // ���� ���Ե�
    [SerializeField]
    private HeroSlot[] _heroSlotArr;

    private void Start()
    {
        HeroSlotsSetting();
    }   
    
    // ���� �������� �� ui������ ������ ���� (���Ե鿡�� �����϶�� ����)
    private void HeroSlotsSetting()
    {
        Dictionary<HEROID, HeroData> heroDataDict = DataManager.Instance.HeroDataDict;

        for (int i = 0; i < _heroSlotArr.Length; i++)
        {
            // ���� ���� �Ϸ� ��Ȳ�϶�, ȣ���� ClosePopup�� �Բ� ���ڷ� �Ѱ��ֱ� 
            _heroSlotArr[i].UIInfoSetting(heroDataDict[(HEROID)i], ClosePopup);
        }
    }

    // �˾� �ݱ�
    private void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}
