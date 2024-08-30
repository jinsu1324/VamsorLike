using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class CharacterDataManager : SerializedMonoBehaviour
{   
    public SLOTNUM SelectSlotNum { get; set; }

    private CharacterData _characterDataSlot01;
    private CharacterData _characterDataSlot02;
    private CharacterData _characterDataSlot03;

    // private static CharacterData _characterData { get { return GetCharacterDataBySlot(_selectSlotNum); } }
        

    // 슬롯번호에 맞게 캐릭터 데이터를 반환해주는 함수
    public CharacterData GetCharacterDataBySlot(SLOTNUM slotNum)
    {
        if (slotNum == SLOTNUM.First)
            return _characterDataSlot01;
        else if (slotNum == SLOTNUM.Second)
            return _characterDataSlot02;
        else if (slotNum == SLOTNUM.Third)
            return _characterDataSlot03;
        else 
            return null;
    }

    // path에 저장된 캐릭터데이터를 불러와서 슬롯 캐릭터 데이터에 저장해주는 함수
    public void LoadCharacterData()
    {
        for (SLOTNUM i = SLOTNUM.First; i <= SLOTNUM.Third; i++)
        {
            string slotPath = GetSlotPath(i);

            if (i == SLOTNUM.First)
                _characterDataSlot01 = SaveLoadManager.LoadData<CharacterData>(slotPath);
            else if (i == SLOTNUM.Second)
                _characterDataSlot02 = SaveLoadManager.LoadData<CharacterData>(slotPath);
            else if (i == SLOTNUM.Third)
                _characterDataSlot03 = SaveLoadManager.LoadData<CharacterData>(slotPath);
        }
    }

    // 새로운 캐릭터 만들고 path에 저장해주는 함수
    public void MakeNewCharacterData(SLOTNUM slotNum, CharacterData myCharacterData)
    {
        string slotPath = GetSlotPath(slotNum);

        if (slotNum == SLOTNUM.First)
        {
            _characterDataSlot01 = myCharacterData;
            SaveLoadManager.SaveData<CharacterData>(_characterDataSlot01, slotPath);
        }
        else if (slotNum == SLOTNUM.Second)
        {
            _characterDataSlot02 = myCharacterData;
            SaveLoadManager.SaveData<CharacterData>(_characterDataSlot02, slotPath);
        }
        else if (slotNum == SLOTNUM.Third)
        {
            _characterDataSlot03 = myCharacterData;
            SaveLoadManager.SaveData<CharacterData>(_characterDataSlot03, slotPath);
        }
    }

    // 슬롯번호에 맞게 path를 반환해주는 함수
    private string GetSlotPath(SLOTNUM slotNum)
    {
        return Path.Combine(Application.persistentDataPath, $"{slotNum}_saveData.json");
    }

}
