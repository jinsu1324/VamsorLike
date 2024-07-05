using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Inventory _invenState;
    private Item _slotItem;

    private void Start()
    {
        BindSlotItem(transform.GetComponentInChildren<Item>());
    }

    // 슬롯내부의 아이템을 슬롯에있는 아이템이라고 바인딩
    public void BindSlotItem(Item item)
    {        
        _slotItem = item;
    }

    // 마우스 눌렀을 때 1회 호출
    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.MouseDown(_slotItem);
    }

    // 마우스 뗐을 때 1회 호출
    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.MouseUp(_slotItem);
    }
}
