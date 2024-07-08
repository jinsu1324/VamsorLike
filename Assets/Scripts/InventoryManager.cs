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

    // '�巡�׾�����'�� ������, '�巡�׾������� ������'�� '���콺������'�� �־ ���콺�� ����ٴϰ� ����
    private static void DragItemFollowMouse()
    {
        if (_dragItem)
            _dragItem.gameObject.transform.position = Input.mousePosition;
    }


    // '���� ���콺 ��ġ'�� '� ����'���� �ִ��� ã��, '�� ����'�� ��ȯ
    public static Slot FindSlotUnderMouse()
    {
        // ���� ���콺 �Ʒ��ִ� �κ��丮�� ���ٸ�, �׳� ��ȯ
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
                Debug.Log("�����Ͱ� �� �ִ� ���� : " + slot);
                return slot;
            }
        }


        Debug.Log("�ƹ��͵� ã�� ���߽��ϴ�");
        return null;
    }


    // ���콺 ������ �� ó��
    public static void MouseDown(Item item)
    {        
        _dragItem = item;
        _startPos = item.gameObject.transform.position;
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        _dragItem.GetComponent<Image>().raycastTarget = false;        
    }


    // ���콺 ���� �� ó��
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
