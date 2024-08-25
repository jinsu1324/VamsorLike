using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{    
    private static Item _dragItem;
    public static Inventory MousePosInventory { get; set; }
    public static Vector3 StartPos { get; set; }
    public static Slot StartSlot { get; set; }
    public static List<InventoryItemData> InventoryItemDataList { get; set; } = new List<InventoryItemData>();
      
    private void Update()
    {
        DragItemFollowMouse();
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

    // 드래그아이템이 마우스 따라다니게
    private static void DragItemFollowMouse()
    {
        if (_dragItem)
            _dragItem.gameObject.transform.position = Input.mousePosition;
    }    

    // 드래그아이템 세팅
    public static void DragItemSetting(Item item)
    {
        _dragItem = item;
        StartPos = item.gameObject.transform.position;
        StartSlot = item.transform.parent.GetComponent<Slot>();
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        _dragItem.GetComponent<Image>().raycastTarget = false;
    }

    // 현재 마우스가 어떤 슬롯위에 있는지 찾고, 그 슬롯을 반환
    public static Slot FindSlotUnderMouse()
    {
        if (MousePosInventory == null)
        {
            Debug.Log("마우스 위치에 아무 인벤토리도 없습니다.");
            return null;
        }

        foreach (Slot slot in MousePosInventory.SlotArr)
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
        FindSlotUnderMouse().SlotItem = _dragItem;
        FindSlotUnderMouse().SlotItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        FindSlotUnderMouse().SlotItem.GetComponent<Image>().raycastTarget = true;
        FindSlotUnderMouse().SlotItem.ItemInventory = FindSlotUnderMouse().SlotInventory;
    }

    // 드래그아이템을 원래슬롯으로 복귀
    public static void MoveItemToOriginSlot()
    {
        _dragItem.transform.SetParent(StartSlot.transform);
        _dragItem.transform.position = StartPos;
        _dragItem.GetComponent<Image>().raycastTarget = true;
    }
}
