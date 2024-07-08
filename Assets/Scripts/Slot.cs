using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Inventory _slotInventory;
    public Item _slotItem;

    private void Start()
    {
        PutInSlotItem(transform.GetComponentInChildren<Item>());
    }

    // '�Ű������� �޾ƿ� ������' ��, '���� ������ ������' �ڸ��� �־���.
    public void PutInSlotItem(Item item)
    {        
        _slotItem = item;
    }

    // ���Կ� ���콺 ������ ��, InventoryManager�� MouseDown�� �����ϰ�, '���� ������ ������' �� �Ű������� �Ѱ��� (1ȸ ȣ��)
    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.MouseDown(_slotItem);
    }

    // ���Կ� ���콺 ���� ��, InventoryManager�� MouseDown�� �����ϰ�, '���� ������ ������' �� �Ű������� �Ѱ��� (1ȸ ȣ��)
    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.MouseUp(_slotItem);
    }
}
