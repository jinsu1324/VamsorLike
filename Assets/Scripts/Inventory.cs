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
        // 현재 인벤토리의 슬롯들을, 슬롯배열에 할당
        int index = 0;        
        foreach (Slot slot in GetComponentsInChildren<Slot>())
        {
            slot._invenState = this;
            slots[index] = slot;
            index++;
        }
            
    }

    // 마우스가 인벤토리에 들어왔을 때 1회 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager._invenState = this;
    }

    // 마우스가 인벤토리에서 나갔을 때 1회 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager._invenState = null;
    }

    
}
