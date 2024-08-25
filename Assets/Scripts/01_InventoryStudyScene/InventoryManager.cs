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

    // �巡�׾������� ���콺 ����ٴϰ�
    private static void DragItemFollowMouse()
    {
        if (_dragItem)
            _dragItem.gameObject.transform.position = Input.mousePosition;
    }    

    // �巡�׾����� ����
    public static void DragItemSetting(Item item)
    {
        _dragItem = item;
        StartPos = item.gameObject.transform.position;
        StartSlot = item.transform.parent.GetComponent<Slot>();
        _dragItem.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        _dragItem.GetComponent<Image>().raycastTarget = false;
    }

    // ���� ���콺�� � �������� �ִ��� ã��, �� ������ ��ȯ
    public static Slot FindSlotUnderMouse()
    {
        if (MousePosInventory == null)
        {
            Debug.Log("���콺 ��ġ�� �ƹ� �κ��丮�� �����ϴ�.");
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

        Debug.Log("�ƹ��� ���Ե� ã�� ���߽��ϴ�");
        return null;
    }

    // �巡�׾������� Ÿ�ٽ������� �ű�
    public static void MoveItemToTargetSlot()
    {
        _dragItem.transform.SetParent(FindSlotUnderMouse().transform);
        FindSlotUnderMouse().SlotItem = _dragItem;
        FindSlotUnderMouse().SlotItem.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        FindSlotUnderMouse().SlotItem.GetComponent<Image>().raycastTarget = true;
        FindSlotUnderMouse().SlotItem.ItemInventory = FindSlotUnderMouse().SlotInventory;
    }

    // �巡�׾������� ������������ ����
    public static void MoveItemToOriginSlot()
    {
        _dragItem.transform.SetParent(StartSlot.transform);
        _dragItem.transform.position = StartPos;
        _dragItem.GetComponent<Image>().raycastTarget = true;
    }
}
