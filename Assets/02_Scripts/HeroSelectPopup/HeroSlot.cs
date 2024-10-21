using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

// ���� ���� : ���� ������ / ���� ���� �Ϸ� Ȯ�� �� ó�� (���ӽ��� ���, �˾� �ݱ� �׼�)
public class HeroSlot : SerializedMonoBehaviour
{
    // ui �� ǥ�õ� ���� ������
    [SerializeField]
    private Image _heroImage;
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private TextMeshProUGUI _descText;

    // ���� �Ϸ� ��ư
    [SerializeField]
    private Button _selectCompleteButton;

    // �� ������ ���� ������
    private HeroData _HeroData;

    // ���� �Ϸ��Ȳ�� �� ȣ���ų �׼�
    private Action _onSelectFinish;    


    // UI ������ ����
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

    // ���� �Ϸ� ��ư �������� ȣ��� �Լ�
    public void OnClickSelectCompleteButton()
    {
        // �� ���Կ����� ID�� HeroID enum ������ ��ȯ
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), _HeroData.Id);

        // �̹����ӿ������� ���õ� ���� ���� �� ����
        PlaySceneManager.Instance.ThisGameHeroSetting(heroID);

        // ī�޶� ����ٴ� Ÿ�� ����
        Camera.main.GetComponent<CameraFollow>().SetFollowTarget(PlaySceneManager.ThisGameHeroObject);

        // ��ų �����˾� ON
        PlaySceneManager.Instance.PlaySceneCanvas.SkillChoicePopupUI.OpenSkillPopup();

        // �������� �˾� �ݱ�
        _onSelectFinish();
    }
}
