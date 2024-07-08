using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Slot[] slots = new Slot[4];   
    public Slot[] Slots { get { return slots; } set { slots = value; } }
       

    private void Start()
    {
        PutChildsInSlots();
    }


    // 현재 마우스가 위치해있는 인벤토리를, 이 인벤토리로 설정 (마우스가 인벤토리에 들어왔을 때 1회 호출)
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager._mouseUnderInventory = this; 
    }


    // 현재 마우스가 위치해있는 인벤토리를, 이 null로 설정 (마우스가 인벤토리에서 나갔을 때 1회 호출 )
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager._mouseUnderInventory = null; 
    }


    // 현재 인벤토리 자식에 있는 모든 슬롯들을 slots 배열에 넣음
    private void PutChildsInSlots()
    {
        int index = 0;
        foreach (Slot slot in GetComponentsInChildren<Slot>())
        {
            slot._slotInventory = this;
            slots[index] = slot;
            index++;
        }
    }
    
}
