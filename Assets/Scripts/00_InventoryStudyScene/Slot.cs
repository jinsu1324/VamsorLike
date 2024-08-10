using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Inventory _slotInventory;
    public Inventory SlotInventory { get { return _slotInventory; } set { _slotInventory = value; } }

    private Item _slotItem;
    public Item SlotItem { get { return _slotItem; } set { _slotItem = value; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.MouseDown(SlotItem);
    }
  
    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.MouseUp(SlotItem);
    }
}
