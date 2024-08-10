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
    public string Name;
    public Sprite Hair;
    public Sprite Face;
    public Sprite Costume;
}

public class CharacterDataManager
{   
    private static SlotNum _selectSlotNum;
    public static SlotNum SelectSlotNum { set { _selectSlotNum = value; } }
    private static CharacterData _characterData { get { return GetCharacterDataBySlot(_selectSlotNum); } }

    private static CharacterData _characterDataSlot01;
    private static CharacterData _characterDataSlot02;
    private static CharacterData _characterDataSlot03;    

    private static string GetSlotPath(SlotNum slotNum)
    {
        return Path.Combine(Application.persistentDataPath, $"{slotNum}_saveData.json");
    }

    public static CharacterData GetCharacterDataBySlot(SlotNum slotNum)
    {
        if (slotNum == SlotNum.First)
            return _characterDataSlot01;
        else if (slotNum == SlotNum.Second)
            return _characterDataSlot02;
        else if (slotNum == SlotNum.Third)
            return _characterDataSlot03;
        else 
            return null;
    }

    public static void LoadCharacterData()
    {
        for (SlotNum i = SlotNum.First; i <= SlotNum.Third; i++)
        {
            string slotPath = GetSlotPath(i);

            if (i == SlotNum.First)
                _characterDataSlot01 = SaveLoadManager_CharacterData.LoadData(slotPath);
            else if (i == SlotNum.Second)
                _characterDataSlot02 = SaveLoadManager_CharacterData.LoadData(slotPath);
            else if (i == SlotNum.Third)
                _characterDataSlot03 = SaveLoadManager_CharacterData.LoadData(slotPath);
        }
    }

    public static void MakeNewCharacterData(SlotNum slotNum, CharacterData myCharacterData)
    {
        string slotPath = GetSlotPath(slotNum);

        if (slotNum == SlotNum.First)
        {
            _characterDataSlot01 = myCharacterData;
            SaveLoadManager_CharacterData.SaveData(_characterDataSlot01, slotPath);
        }
        else if (slotNum == SlotNum.Second)
        {
            _characterDataSlot02 = myCharacterData;
            SaveLoadManager_CharacterData.SaveData(_characterDataSlot02, slotPath);
        }
        else if (slotNum == SlotNum.Third)
        {
            _characterDataSlot03 = myCharacterData;
            SaveLoadManager_CharacterData.SaveData(_characterDataSlot03, slotPath);
        }
    }
    
}
