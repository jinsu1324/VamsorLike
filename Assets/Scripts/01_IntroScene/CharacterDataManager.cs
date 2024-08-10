using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class CharacterDataManager : SerializedMonoBehaviour
{   
    private SlotNum _selectSlotNum;
    public SlotNum SelectSlotNum { set { _selectSlotNum = value; } }

    private CharacterData _characterDataSlot01;
    private CharacterData _characterDataSlot02;
    private CharacterData _characterDataSlot03;

    // private static CharacterData _characterData { get { return GetCharacterDataBySlot(_selectSlotNum); } }
        

    // 슬롯번호에 맞게 캐릭터 데이터를 반환해주는 함수
    public CharacterData GetCharacterDataBySlot(SlotNum slotNum)
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

    // path에 저장된 캐릭터데이터를 불러와서 슬롯 캐릭터 데이터에 저장해주는 함수
    public void LoadCharacterData()
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

    // 새로운 캐릭터 만들고 path에 저장해주는 함수
    public void MakeNewCharacterData(SlotNum slotNum, CharacterData myCharacterData)
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

    // 슬롯번호에 맞게 path를 반환해주는 함수
    private string GetSlotPath(SlotNum slotNum)
    {
        return Path.Combine(Application.persistentDataPath, $"{slotNum}_saveData.json");
    }

}
