using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Inventory _itemCurrentInventory;

    public void OnPointerDown(PointerEventData eventData)
    {        
        InventoryManager.MouseDown(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {        
        InventoryManager.MouseUp(this);
    }

    
}
