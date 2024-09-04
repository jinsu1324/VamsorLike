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
        Dictionary<HeroID, HeroData> heroDataDict = DataManager.Instance.HeroDataDict;

        for (int i = 0; i < _heroSlotArr.Length; i++)
        {
            // ���� ���� �Ϸ� ��Ȳ�϶�, ȣ���� ClosePopup�� �Բ� ���ڷ� �Ѱ��ֱ� 
            _heroSlotArr[i].UIInfoSetting(heroDataDict[(HeroID)i], ClosePopup);
        }
    }

    // �˾� ����
    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }


    // �˾� �ݱ�
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
