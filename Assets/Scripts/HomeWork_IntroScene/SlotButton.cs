using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotNum
{
    First,
    Second,
    Third
}


public class SlotButton : MonoBehaviour, IPointerClickHandler
{
    public SlotNum _slotNum;

    [HideInInspector]
    public string _slotPath;

    [HideInInspector]
    public CharacterData _characterData;

    public Action<CharacterData> ClickAction;


    private void Start()
    {
        _slotPath = Path.Combine(Application.persistentDataPath, $"{_slotNum}_saveData.json");

        ClickAction += CharacterDataFindBinding;

        _characterData = GetComponent<SlotData>()._characterData;
        
        if (File.Exists(_slotPath))
        {
            Debug.Log($"{_slotNum}_세이브 데이터 있어서 그거 가져와서 할당");
            _characterData = SlotSaveLoadManager.LoadData(_slotPath);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        ClickAction(_characterData);
    }


    public void CharacterDataFindBinding(CharacterData characterData)
    {
        if (characterData == null)
        {
            characterData = new CharacterData() { _name = $"{_slotNum}_캐릭터" };

            Debug.Log("데이터가 없다(null이다)");
            Debug.Log(characterData._name);

            SlotSaveLoadManager.SaveData(characterData, _slotNum, _slotPath);
            _characterData = SlotSaveLoadManager.LoadData(_slotPath);

        }
        else if (characterData != null)
        {
            Debug.Log("데이터가 있다(null아니다)");
            Debug.Log(characterData._name);
        }
    }
}
