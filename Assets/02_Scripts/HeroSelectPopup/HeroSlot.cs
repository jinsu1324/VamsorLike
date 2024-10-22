using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

// ���� ���� : ���� ������ / ���� ���� �Ϸ� Ȯ�� �� ó�� (���ӽ��� ���, �˾� �ݱ� �׼�)
public class HeroSlot : MonoBehaviour
{    
    [SerializeField]
    private Image _heroImage;                       // ui �� ǥ�õ� ���� �̹���
    [SerializeField]
    private TextMeshProUGUI _nameText;              // ui �� ǥ�õ� ���� �̸� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI _descText;              // ui �� ǥ�õ� ���� ���� �ؽ�Ʈ

    [SerializeField]
    private Button _selectCompleteButton;           // ���� �Ϸ� ��ư
                                                     
    private HeroData _HeroData;                     // �� ������ ���� ������
    
    private Action _onSelectFinish;                 // ���� �Ϸ��Ȳ�� �� ȣ���ų �׼�


    /// <summary>
    /// UI ������ ����
    /// </summary>
    public void UIInfoSetting(HeroData heroData, Action popupClose)
    {
        // �� ���� ���������� ����
        _HeroData = heroData;
        
        // UI ������ ����
        _heroImage.sprite = heroData.Sprite;
        _nameText.text = heroData.Name;
        _descText.text = heroData.Desc;

        _onSelectFinish += popupClose;

        // ���� �Ϸ� ��ư �������� ȣ���� �Լ� ���
        _selectCompleteButton.onClick.AddListener(OnClickSelectCompleteButton);
    }

    /// <summary>
    /// ���� �Ϸ� ��ư �������� ȣ��� �Լ�
    /// </summary>
    public void OnClickSelectCompleteButton()
    {       
        // �� ���Կ����� ID�� HeroID enum ������ ��ȯ
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), _HeroData.Id);

        // �̹����ӿ������� ���õ� ���� ���� �� ����
        GameManager.Instance.MyHeroIDSetting(heroID);

        // �������� �˾� �ݱ�
        _onSelectFinish();

        // �� ��ȯ
        SceneSwitcher.LoadScene("02_PlayScene");
    }
}
