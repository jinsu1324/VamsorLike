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


    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager._mousePosInventory = this; 
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager._mousePosInventory = null; 
    }


    // 이 인벤토리 자식에 있는 모든 슬롯들을, slots 배열에 넣고, 인벤토리도 모두 할당
    private void PutChildsInSlots()
    {
        int index = 0;
        foreach (Slot slot in GetComponentsInChildren<Slot>())
        {
            slot._slotInventory = this;
            slot._slotItem = slot.transform.GetComponentInChildren<Item>();
            if (slot._slotItem != null)
            {
                slot._slotItem._itemInventory = this;
            }
                
            slots[index] = slot;
            index++;
        }
    }    
}
