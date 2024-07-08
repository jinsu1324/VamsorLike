using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{    
    private static Item _dragItem;
    public static Inventory _mouseUnderInventory;
    public static Vector3 _startPos;

    private void Update()
    {
        DragItemFollowMouse();
    }

    // '드래그아이템'이 있을때, '드래그아이템의 포지션'에 '마우스포지션'을 넣어서 마우스를 따라다니게 해줌
    private static void DragItemFollowMouse()
    {
        if (_dragItem)
            _dragItem.gameObject.transform.position = Input.mousePosition;
    }


    // '현재 마우스 위치'가 '어떤 슬롯'위에 있는지 찾고, '그 슬롯'을 반환
    public static Slot FindSlotUnderMouse()
    {
        // 현재 마우스 아래있는 인벤토리가 없다면, 그냥 반환
        if (_mouseUnderInventory == null)
        {
            return null;
        }

        
        foreach (Slot slot in _mouseUnderInventory.Slots)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                slot.GetComponent<RectTransform>(),
                Input.mousePosition))
            {
                Debug.Log("포인터가 들어가 있는 슬롯 : " + slot);
                return slot;
            }
        }


        Debug.Log("아무것도 찾지 못했습니다");
        return null;
    }


    // 마우스 눌렀을 때 처리
    public static void MouseDown(Item item)
    {        
        _dragItem = item;
        _startPos = item.gameObject.transform.position;
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        _dragItem.GetComponent<Image>().raycastTarget = false;        
    }


    // 마우스 뗐을 때 처리
    public static void MouseUp(Item item)
    {      
        if (FindSlotUnderMouse() == null)
        {
            Debug.Log(_dragItem._itemInventory);
            if (_mouseUnderInventory == _dragItem._itemInventory || _mouseUnderInventory == null)
            {
                _dragItem.transform.SetParent(_dragItem._itemInventory.transform);
                _dragItem.transform.position = _startPos;
                _dragItem.GetComponent<Image>().raycastTarget = true;
            }
            else
            {
                _dragItem.transform.SetParent(_mouseUnderInventory.gameObject.transform);
                _dragItem._itemInventory = _mouseUnderInventory;
                _dragItem.GetComponent<Image>().raycastTarget = true;
            }
        }


        if (FindSlotUnderMouse() != null)
        {
            FindSlotUnderMouse()._slotItem = _dragItem;
            FindSlotUnderMouse()._slotItem.transform.SetParent(FindSlotUnderMouse().transform);
            FindSlotUnderMouse()._slotItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            FindSlotUnderMouse()._slotItem.GetComponent<Image>().raycastTarget = true;
            FindSlotUnderMouse().PutInSlotItem(FindSlotUnderMouse().transform.GetComponentInChildren<Item>());
        }

        _dragItem = null;
    }
}
