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
    [SerializeField]
    private Button _selectButton;

    // �� ������ ���� ������
    private HeroData _heroData;

    // �˾� ������ ����� �׼�
    private Action _popUpFinishAction;

    // ���� UI ������ �ʱ�ȭ
    public void InitInfoUI(HeroData heroData, Action finishAction)
    {
        _heroData = heroData;

        _heroImage.sprite = heroData.Sprite;
        _nameText.text = heroData.Name;

        // �˾� ����ɶ� �׼� ���
        _popUpFinishAction = finishAction;

        // ���ù�ư �������� �׼� ���
        _selectButton.onClick.AddListener(OnClickSelectButton);
    }

    // ���ù�ư ��������
    public void OnClickSelectButton()
    {
        // �� �������� ���ӽ���
        PlaySceneManager.PlayStart(_heroData);

        // �˾� ����׼� ����
        _popUpFinishAction();
    }
}
