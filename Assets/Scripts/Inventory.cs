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


    // �� �κ��丮 �ڽĿ� �ִ� ��� ���Ե���, slots �迭�� �ְ�, �κ��丮�� ��� �Ҵ�
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
