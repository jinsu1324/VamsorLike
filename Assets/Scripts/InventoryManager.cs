using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{    
    private static Item _dragItem;
    public static Inventory _mouseCurrentInventory;
    public static Vector3 _itemStartPos;

    private void Update()
    {
        Debug.Log(_mouseCurrentInventory);
        if (_dragItem)
            _dragItem.gameObject.transform.position = Input.mousePosition;
    }

    public static void MouseDown(Item item)
    {
        _dragItem = item;
        _itemStartPos = item.gameObject.transform.position;
        _dragItem.GetComponent<Image>().raycastTarget = false;
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
    }

    public static void MouseUp(Item item)
    {        
        if (_mouseCurrentInventory == _dragItem._itemCurrentInventory)
        {
            _dragItem.transform.position = _itemStartPos;
            _dragItem.GetComponent<Image>().raycastTarget = true;
            Debug.Log("재자리!");

        }
        else if (_mouseCurrentInventory != _dragItem._itemCurrentInventory)
        {            
            _dragItem.transform.SetParent(_mouseCurrentInventory.gameObject.transform);
            _dragItem._itemCurrentInventory = _mouseCurrentInventory;
            _dragItem.GetComponent<Image>().raycastTarget = true;
            Debug.Log("옮김!");

        }

        //_dragItem.transform.position = Input.mousePosition;
        _dragItem = null;
        //Debug.Log("마우스업!");
    }
}
