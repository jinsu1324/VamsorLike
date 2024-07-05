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

    // 마우스 눌렀을 때 처리함수
    public static void MouseDown(Item item)
    {        
        _dragItem = item;
        Debug.Log("_dragItem : " + _dragItem);
        _startPos = item.gameObject.transform.position;
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        _dragItem.GetComponent<Image>().raycastTarget = false;        
    }

    // 마우스 뗐을 때 처리함수
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

        // 마우스 포인터가 슬롯에 들어가있는지 체크
        foreach (Slot slot in _invenState.Slots)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(
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
}
