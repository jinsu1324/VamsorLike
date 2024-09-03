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

    // ���� �Ϸ� ��ư
    [SerializeField]
    private Button _selectCompleteButton;

    // �� ������ ���� ������
    private HeroData _HeroData;

    #region ���� �Ϸ�Ǿ����� �ؾ��Ұ͵�
    // 1. �˾� �ݱ�
    // 2. �ش� ������ �������� ���� ����
    // 3. �� ���� �ʵ忡 �����ϰ� ���ȵ� �־���
    #endregion
    // ���� �Ϸ��Ȳ�� �� ȣ���ų �׼�
    public static event Action OnHeroSelectComplete;    


    // UI ������ ����
    public void UIInfoSetting(HeroData heroData, Action closeAction)
    {
        // �� ���� ���������� ����
        _HeroData = heroData;
        
        // UI ������ ����
        _heroImage.sprite = heroData.Sprite;
        _nameText.text = heroData.Name;

        OnHeroSelectComplete += closeAction;

        // ���� �Ϸ� ��ư �������� ȣ���� �Լ� ���
        _selectCompleteButton.onClick.AddListener(OnClickSelectCompleteButton);
    }

    // ���� �Ϸ� ��ư �������� ȣ��� �Լ�
    public void OnClickSelectCompleteButton()
    {
        // �� ���Կ����� ID�� HeroID enum ������ ��ȯ
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), _HeroData.Id);

        // ���ӽ��۵��� true��
        PlaySceneManager.Instance.IsGameStartTrue();

        // �̹����ӿ������� ���õ� ���� ���� �� ����
        PlaySceneManager.Instance.ThisGameHeroSetting(heroID);

        // ���� �Ϸ��Ȳ �׼� ���� (�������� �˾� �ݱ�)
        OnHeroSelectComplete();

        // ���� ���� ����
        MonsterSpawner.Instance.StartMonsterSpawn();
    }
}
