using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� ���� �˾� : ���� ���Ե� / �� �������Ե� �������ʱ�ȭ ��� / �˾� �ݱ�
public class HeroSelectPopup : SerializedMonoBehaviour
{    
    [SerializeField]
    private HeroSelectPopup_Slot[] _heroSlotArr;                // ���� ���Ե�

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        HeroSlotsSetting();
    }   
    
    /// <summary>
    /// ���� �������� �� ui������ ������ ���� (���Ե鿡�� �����϶�� ����)
    /// </summary>
    private void HeroSlotsSetting()
    {
        // �̻��
        //Dictionary<HeroID, HeroData> heroDataDict = DataManager.Instance.HeroDataDict;

        //HeroDatas heroDatas = DataManager.Instance.HeroDatas;


        //for (int i = 0; i < _heroSlotArr.Length; i++)
        //{
        //    // ���� ���� �Ϸ� ��Ȳ�϶�, ȣ���� ClosePopup�� �Բ� ���ڷ� �Ѱ��ֱ� 
        //    _heroSlotArr[i].UIInfoSetting(heroDataDict[(HeroID)i], ClosePopup);
        //}
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� �ݱ�
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
