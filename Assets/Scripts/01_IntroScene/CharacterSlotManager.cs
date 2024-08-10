using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlotManager : MonoBehaviour
{   
    [SerializeField]
    private CharacterSlot[] _characterSlotArr;
    public CharacterSlot[] CharacterSlotArr { get { return _characterSlotArr; } }

    private void Start()
    {
        InitSlots();
    }

    // 캐릭터 슬롯들 초기화
    public void InitSlots()
    {
        for (int i = 0; i < _characterSlotArr.Length; i++)
        {
            // 각각 슬롯마다 슬롯번호 + 캐릭터 데이터 초기화 
            _characterSlotArr[i].SlotNumInit((SlotNum)i);
            _characterSlotArr[i].SlotCharacterDataInit((SlotNum)i);
        }
    }
}
