using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void Start()
    {
        foreach (Item item in GetComponentsInChildren<Item>())
        {
            item._itemCurrentInventory = this;
        }      
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log($"{this.gameObject.name} 인벤토리에 들어옴");
        InventoryManager._mouseCurrentInventory = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log($"{this.gameObject.name} 인벤토리에서 나옴");
    }

    
}
