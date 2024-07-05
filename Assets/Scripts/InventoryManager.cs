using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{    
    private static Item _dragItem;
    public static Inventory _invenState;
    public static Vector3 _startPos;

    private void Update()
    {        
        if (_dragItem)
            _dragItem.gameObject.transform.position = Input.mousePosition;

        
    }

    // ���콺 ������ �� ó���Լ�
    public static void MouseDown(Item item)
    {        
        _dragItem = item;
        Debug.Log("_dragItem : " + _dragItem);
        _startPos = item.gameObject.transform.position;
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        _dragItem.GetComponent<Image>().raycastTarget = false;        
    }

    // ���콺 ���� �� ó���Լ�
    public static void MouseUp(Item item)
    {        
        //if (_invenState == _dragItem._invenState || _invenState == null)
        //{
        //    _dragItem.transform.SetParent(_dragItem._invenState.transform);
        //    _dragItem.transform.position = _startPos;
        //    _dragItem.GetComponent<Image>().raycastTarget = true;  
        //}
        //else
        //{            
        //    _dragItem.transform.SetParent(_invenState.gameObject.transform);
        //    _dragItem._invenState = _invenState;
        //    _dragItem.GetComponent<Image>().raycastTarget = true;
        //}      

        if (_dragItem != null)
        {
            _dragItem.transform.SetParent(CheckMonsePosInSlot().transform);
            _dragItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            _dragItem.GetComponent<Image>().raycastTarget = true;
            CheckMonsePosInSlot().BindSlotItem(CheckMonsePosInSlot().transform.GetComponentInChildren<Item>());
        }

        _dragItem = null;
    }

    public static Slot CheckMonsePosInSlot()
    {
        if (_invenState == null)
            return null;

        // ���콺 �����Ͱ� ���Կ� ���ִ��� üũ
        foreach (Slot slot in _invenState.Slots)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(
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
}
