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
        _startPos = item.gameObject.transform.position;
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        _dragItem.GetComponent<Image>().raycastTarget = false;        
    }

    // 마우스 뗐을 때 처리함수
    public static void MouseUp(Item item)
    {        
        if (_invenState == _dragItem._invenState || _invenState == null)
        {
            _dragItem.transform.SetParent(_dragItem._invenState.transform);
            _dragItem.transform.position = _startPos;
            _dragItem.GetComponent<Image>().raycastTarget = true;  
        }
        else
        {            
            _dragItem.transform.SetParent(_invenState.gameObject.transform);
            _dragItem._invenState = _invenState;
            _dragItem.GetComponent<Image>().raycastTarget = true;
        }      
        _dragItem = null;
    }
}
