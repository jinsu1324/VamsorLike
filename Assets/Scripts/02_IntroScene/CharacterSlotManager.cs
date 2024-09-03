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

    // ĳ���� ���Ե� �ʱ�ȭ
    public void InitSlots()
    {
        for (int i = 0; i < _characterSlotArr.Length; i++)
        {
            // ���� ���Ը��� ���Թ�ȣ + ĳ���� ������ �ʱ�ȭ 
            _characterSlotArr[i].SlotNumInit((SlotNum)i);
            _characterSlotArr[i].SlotCharacterDataInit((SlotNum)i);
        }
    }
}
