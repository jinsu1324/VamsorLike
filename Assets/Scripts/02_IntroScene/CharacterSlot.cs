using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.U2D.Animation;

public enum SLOTNUM
{
    First,
    Second,
    Third
}

public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
    // ���� ��ȣ
    public SLOTNUM SlotNum { get; set; }

    // ���� ĳ���� ������
    public CharacterData SlotCharacterData { get; set; }

    // ui �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI _uiText;

    // ���� Ŭ������ �� �׼�
    private Action _slotClickAction;


    // ���� Ŭ�������� ȣ��
    public void OnPointerClick(PointerEventData eventData)
    {
        _slotClickAction();
    }

    // ���� �ѹ� ���ڿ� �°� �ʱ�ȭ ���ִ� �Լ�
    public void SlotNumInit(SLOTNUM slotNum)
    {
        this.SlotNum = slotNum;
    }

    // ���� ĳ���� ������ �ʱ�ȭ���ִ� �Լ�
    public void SlotCharacterDataInit(SLOTNUM slotNum)
    {
        IntroSceneManager.Instance.CharacterDataManager.LoadCharacterData();
        SlotCharacterData = IntroSceneManager.Instance.CharacterDataManager.GetCharacterDataBySlot(slotNum);

        DataCheckAndChoiceNextAction();
    }

    // �����͸� üũ�ϰ�, �����׼��� �̸� ���ؼ� �׼ǿ� �־��ִ� �Լ� (����â�� ����, ������ ��������)
    public void DataCheckAndChoiceNextAction()
    {
        // �����Ͱ� ������, �����׼ǿ� ĳ���ͻ���â�� ������
        if (SlotCharacterData == null)
        {
            _slotClickAction = OpenCharacterMakePopup;
            _uiText.text = "No Data";
        }
        // �����Ͱ� ������, �����׼ǿ� ���ӽ����� �ϵ���
        else if (SlotCharacterData != null)
        {
            _slotClickAction = GameStart;
            _uiText.text = $"{SlotCharacterData.Name}";
        }
    }

    // ĳ���ͻ��� �˾� ON (ĳ���ͻ����˾����� ����� ���)
    public void OpenCharacterMakePopup()
    {
        Debug.Log("ĳ���� �����˾��� ���ϴ�.");
        IntroSceneManager.Instance.CharacterMakePopup.OpenPopup(SlotNum);
    }

    // ���� ����
    private void GameStart()
    {
        Debug.Log("������ �����մϴ�.");
    }   
}
