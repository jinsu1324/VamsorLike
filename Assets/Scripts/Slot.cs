using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Inventory _invenState;
    private Item _slotItem;

    private void Start()
    {
        BindSlotItem(transform.GetComponentInChildren<Item>());
    }

    // ���Գ����� �������� ���Կ��ִ� �������̶�� ���ε�
    public void BindSlotItem(Item item)
    {        
        _slotItem = item;
    }

    // ���콺 ������ �� 1ȸ ȣ��
    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.MouseDown(_slotItem);
    }

    // ���콺 ���� �� 1ȸ ȣ��
    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.MouseUp(_slotItem);
    }
}
