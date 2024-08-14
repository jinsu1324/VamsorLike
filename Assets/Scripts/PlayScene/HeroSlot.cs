using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSlot : SerializedMonoBehaviour
{
    // ui �� ǥ�õ� ���� ������
    [SerializeField]
    private Image _heroImage;
    [SerializeField]
    private TextMeshProUGUI _nameText;

    // ���� �Ϸ� ��ư
    [SerializeField]
    private Button _selectCompleteButton;

    // �� ������ ���� ������
    private HeroData _HeroData;

    // ���� �Ϸ��Ȳ�� �� ȣ���ų �׼�
    private Action _selectCompleteAction;

    // UI ������ ����
    public void UIInfoSetting(HeroData heroData, Action selectCompleteAction)
    {
        // �� ���� ���������� ����
        _HeroData = heroData;
        
        // UI ������ ����
        _heroImage.sprite = heroData.Sprite;
        _nameText.text = heroData.Name;

        // ���� �Ϸ� ��Ȳ�� �� ȣ���� �׼ǿ� �Լ� ��� (���� �˾� �ݱⰡ ������)
        _selectCompleteAction = selectCompleteAction;

        // ���� �Ϸ� ��ư �������� ȣ���� �Լ� ���
        _selectCompleteButton.onClick.AddListener(OnClickSelectCompleteButton);
    }

    // ���� �Ϸ� ��ư �������� ȣ��� �Լ�
    public void OnClickSelectCompleteButton()
    {
        // �� ���Կ����� ID�� HeroID enum ������ ��ȯ
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), _HeroData.Id);

        // �� ������ �������� ���ӽ���
        PlaySceneManager.Instance.PlayStart(heroID);

        // ���� �Ϸ��Ȳ �׼� ����
        _selectCompleteAction();
    }
}
