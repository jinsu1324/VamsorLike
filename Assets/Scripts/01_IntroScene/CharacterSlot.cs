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

public enum SlotNum
{
    First,
    Second,
    Third
}

public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
    // ���� ��ȣ
    private SlotNum _slotNum; 
    public SlotNum SlotNum { get { return _slotNum; } set { _slotNum = value; } }

    // ���� ĳ���� ������
    private CharacterData _slotCharacterData;
    public CharacterData SlotCharacterData { get { return _slotCharacterData; } set { _slotCharacterData = value; } }

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
    public void SlotNumInit(SlotNum slotNum)
    {
        this._slotNum = slotNum;
    }

    // ���� ĳ���� ������ �ʱ�ȭ���ִ� �Լ�
    public void SlotCharacterDataInit(SlotNum slotNum)
    {
        IntroSceneManager.Instance.CharacterDataManager.LoadCharacterData();
        _slotCharacterData = IntroSceneManager.Instance.CharacterDataManager.GetCharacterDataBySlot(slotNum);

        DataCheckAndChoiceNextAction();
    }

    // �����͸� üũ�ϰ�, �����׼��� �̸� ���ؼ� �׼ǿ� �־��ִ� �Լ� (����â�� ����, ������ ��������)
    public void DataCheckAndChoiceNextAction()
    {
        // �����Ͱ� ������, �����׼ǿ� ĳ���ͻ���â�� ������
        if (_slotCharacterData == null)
        {
            _slotClickAction = OpenCharacterMakePopup;
            _uiText.text = "No Data";
        }
        // �����Ͱ� ������, �����׼ǿ� ���ӽ����� �ϵ���
        else if (_slotCharacterData != null)
        {
            _slotClickAction = GameStart;
            _uiText.text = $"{_slotCharacterData.Name}";
        }
    }

    // ĳ���ͻ��� �˾� ON (ĳ���ͻ����˾����� ����� ���)
    public void OpenCharacterMakePopup()
    {
        Debug.Log("ĳ���� �����˾��� ���ϴ�.");
        IntroSceneManager.Instance.CharacterMakePopup.OpenPopup(_slotNum);
    }

    // ���� ����
    private void GameStart()
    {
        Debug.Log("������ �����մϴ�.");
    }   
}
