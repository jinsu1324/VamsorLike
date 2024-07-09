using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{    
    private static Item _dragItem;
    public static Inventory _mousePosInventory;
    public static Vector3 _startPos;
    public static Slot _startSlot;

    private List<InventoryItemData> _inventoryItemDatas = new List<InventoryItemData>();

    private void Start()
    {
        ConsumeInventoryItemData consumeInventoryItemData = new ConsumeInventoryItemData();
        EquipInventoryItemData equipInventoryItemData = new EquipInventoryItemData();

        consumeInventoryItemData._itemID = "회복물약 001";
        equipInventoryItemData._itemID = "전사장비 001";

        _inventoryItemDatas.Add(consumeInventoryItemData);
        _inventoryItemDatas.Add(equipInventoryItemData);

        foreach (InventoryItemData item in _inventoryItemDatas)
        {
            ConsumeInventoryItemData consumeInventoryItem = (ConsumeInventoryItemData)item;

            consumeInventoryItem.HI();

            Debug.Log(item.ShowInfo());
            Debug.Log(item.GetType());
        }        
    }

    private void Update()
    {
        DragItemFollowMouse();
    }


    // 드래그아이템이 마우스 따라다니게
    private static void DragItemFollowMouse()
    {
        if (_dragItem)
            _dragItem.gameObject.transform.position = Input.mousePosition;
    }    


    // 마우스 눌렀을 때 처리
    public static void MouseDown(Item item)
    {
        DragItemSetting(item); // 드래그아이템 셋팅
    }


    // 마우스 뗐을 때 처리
    public static void MouseUp(Item item)
    {        
        if (FindSlotUnderMouse() != null) 
            MoveItemToTargetSlot(); // 마우스위치에 슬롯이 있다면, 드래그아이템 그 슬롯으로 이동
        else 
            MoveItemToOriginSlot(); // 마우스위치에 슬롯이 있다면, 드래그아이템 다시 원래슬롯으로 복귀

        _dragItem = null;
    }


    // 드래그아이템 세팅
    public static void DragItemSetting(Item item)
    {
        _dragItem = item;
        _startPos = item.gameObject.transform.position;
        _startSlot = item.transform.parent.GetComponent<Slot>();
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        _dragItem.GetComponent<Image>().raycastTarget = false;
    }


    // 현재 마우스가 어떤 슬롯위에 있는지 찾고, 그 슬롯을 반환
    public static Slot FindSlotUnderMouse()
    {
        if (_mousePosInventory == null)
        {
            Debug.Log("마우스 위치에 아무 인벤토리도 없습니다.");
            return null;
        }

        foreach (Slot slot in _mousePosInventory.Slots)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                slot.GetComponent<RectTransform>(),
                Input.mousePosition))
            {
                return slot;
            }
        }

        Debug.Log("아무것 슬롯도 찾지 못했습니다");
        return null;
    }


    // 드래그아이템을 타겟슬롯으로 옮김
    public static void MoveItemToTargetSlot()
    {
        _dragItem.transform.SetParent(FindSlotUnderMouse().transform);
        FindSlotUnderMouse()._slotItem = _dragItem;
        FindSlotUnderMouse()._slotItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        FindSlotUnderMouse()._slotItem.GetComponent<Image>().raycastTarget = true;
        FindSlotUnderMouse()._slotItem._itemInventory = FindSlotUnderMouse()._slotInventory;
    }


    // 드래그아이템을 원래슬롯으로 복귀
    public static void MoveItemToOriginSlot()
    {
        _dragItem.transform.SetParent(_startSlot.transform);
        _dragItem.transform.position = _startPos;
        _dragItem.GetComponent<Image>().raycastTarget = true;
    }
}
