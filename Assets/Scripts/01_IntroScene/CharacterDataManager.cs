using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;


public enum CharacterParts
{
    Hair,
    Face,
    Costume
}

public class CharacterData
{
    public string _name;
    public Sprite _hair;
    public Sprite _face;
    public Sprite _costume;
}


public class CharacterDataManager
{
    private static CharacterData _characterData_Slot01;
    private static CharacterData _characterData_Slot02;
    private static CharacterData _characterData_Slot03;

    private static SlotNum _selectSlotNum;
    public static SlotNum SelectSlotNum { set { _selectSlotNum = value; } } 
    private static CharacterData _characterData { get { return GetCharacterDataBySlot(_selectSlotNum); } }

    private static string GetSlotPath(SlotNum slotNum)
    {
        return Path.Combine(Application.persistentDataPath, $"{slotNum}_saveData.json");
    }

    public static CharacterData GetCharacterDataBySlot(SlotNum slotNum)
    {
        if (slotNum == SlotNum.First)
            return _characterData_Slot01;
        else if (slotNum == SlotNum.Second)
            return _characterData_Slot02;
        else if (slotNum == SlotNum.Third)
            return _characterData_Slot03;
        else 
            return null;
    }

    public static void LoadCharacterData()
    {
        for (SlotNum i = SlotNum.First; i <= SlotNum.Third; i++)
        {
            string slotPath = GetSlotPath(i);

            if (i == SlotNum.First)
                _characterData_Slot01 = SaveLoadManager_CharacterData.LoadData(slotPath);
            else if (i == SlotNum.Second)
                _characterData_Slot02 = SaveLoadManager_CharacterData.LoadData(slotPath);
            else if (i == SlotNum.Third)
                _characterData_Slot03 = SaveLoadManager_CharacterData.LoadData(slotPath);
        }
    }


    public static void MakeNewCharacterData(SlotNum slotNum, CharacterData myCharacterData)
    {
        string slotPath = GetSlotPath(slotNum);

        if (slotNum == SlotNum.First)
        {
            _characterData_Slot01 = myCharacterData;
            SaveLoadManager_CharacterData.SaveData(_characterData_Slot01, slotPath);
        }
        else if (slotNum == SlotNum.Second)
        {
            _characterData_Slot02 = myCharacterData;
            SaveLoadManager_CharacterData.SaveData(_characterData_Slot02, slotPath);
        }
        else if (slotNum == SlotNum.Third)
        {
            _characterData_Slot03 = myCharacterData;
            SaveLoadManager_CharacterData.SaveData(_characterData_Slot03, slotPath);
        }
    }
    
}
