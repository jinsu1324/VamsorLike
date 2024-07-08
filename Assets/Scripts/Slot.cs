using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Inventory _slotInventory;
    public Item _slotItem;

    private void Start()
    {
        PutInSlotItem(transform.GetComponentInChildren<Item>());
    }

    // '매개변수로 받아온 아이템' 을, '현재 슬롯의 아이템' 자리에 넣어줌.
    public void PutInSlotItem(Item item)
    {        
        _slotItem = item;
    }

    // 슬롯에 마우스 눌렀을 때, InventoryManager의 MouseDown을 실행하고, '현재 슬롯의 아이템' 을 매개변수로 넘겨줌 (1회 호출)
    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.MouseDown(_slotItem);
    }

    // 슬롯에 마우스 뗐을 때, InventoryManager의 MouseDown을 실행하고, '현재 슬롯의 아이템' 을 매개변수로 넘겨줌 (1회 호출)
    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.MouseUp(_slotItem);
    }
}
