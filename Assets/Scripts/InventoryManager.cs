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

        consumeInventoryItemData._itemID = "ȸ������ 001";
        equipInventoryItemData._itemID = "������� 001";

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


    // �巡�׾������� ���콺 ����ٴϰ�
    private static void DragItemFollowMouse()
    {
        if (_dragItem)
            _dragItem.gameObject.transform.position = Input.mousePosition;
    }    


    // ���콺 ������ �� ó��
    public static void MouseDown(Item item)
    {
        DragItemSetting(item); // �巡�׾����� ����
    }


    // ���콺 ���� �� ó��
    public static void MouseUp(Item item)
    {        
        if (FindSlotUnderMouse() != null) 
            MoveItemToTargetSlot(); // ���콺��ġ�� ������ �ִٸ�, �巡�׾����� �� �������� �̵�
        else 
            MoveItemToOriginSlot(); // ���콺��ġ�� ������ �ִٸ�, �巡�׾����� �ٽ� ������������ ����

        _dragItem = null;
    }


    // �巡�׾����� ����
    public static void DragItemSetting(Item item)
    {
        _dragItem = item;
        _startPos = item.gameObject.transform.position;
        _startSlot = item.transform.parent.GetComponent<Slot>();
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        _dragItem.GetComponent<Image>().raycastTarget = false;
    }


    // ���� ���콺�� � �������� �ִ��� ã��, �� ������ ��ȯ
    public static Slot FindSlotUnderMouse()
    {
        if (_mousePosInventory == null)
        {
            Debug.Log("���콺 ��ġ�� �ƹ� �κ��丮�� �����ϴ�.");
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

        Debug.Log("�ƹ��� ���Ե� ã�� ���߽��ϴ�");
        return null;
    }


    // �巡�׾������� Ÿ�ٽ������� �ű�
    public static void MoveItemToTargetSlot()
    {
        _dragItem.transform.SetParent(FindSlotUnderMouse().transform);
        FindSlotUnderMouse()._slotItem = _dragItem;
        FindSlotUnderMouse()._slotItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        FindSlotUnderMouse()._slotItem.GetComponent<Image>().raycastTarget = true;
        FindSlotUnderMouse()._slotItem._itemInventory = FindSlotUnderMouse()._slotInventory;
    }


    // �巡�׾������� ������������ ����
    public static void MoveItemToOriginSlot()
    {
        _dragItem.transform.SetParent(_startSlot.transform);
        _dragItem.transform.position = _startPos;
        _dragItem.GetComponent<Image>().raycastTarget = true;
    }
}
