using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Inventory _slotInventory;
    public Item _slotItem;

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.MouseDown(_slotItem);
    }
  
    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.MouseUp(_slotItem);
    }
}
