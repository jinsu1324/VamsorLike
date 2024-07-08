using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Slot[] slots = new Slot[4];   
    public Slot[] Slots { get { return slots; } set { slots = value; } }
       

    private void Start()
    {
        PutChildsInSlots();
    }


    // ���� ���콺�� ��ġ���ִ� �κ��丮��, �� �κ��丮�� ���� (���콺�� �κ��丮�� ������ �� 1ȸ ȣ��)
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager._mouseUnderInventory = this; 
    }


    // ���� ���콺�� ��ġ���ִ� �κ��丮��, �� null�� ���� (���콺�� �κ��丮���� ������ �� 1ȸ ȣ�� )
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager._mouseUnderInventory = null; 
    }


    // ���� �κ��丮 �ڽĿ� �ִ� ��� ���Ե��� slots �迭�� ����
    private void PutChildsInSlots()
    {
        int index = 0;
        foreach (Slot slot in GetComponentsInChildren<Slot>())
        {
            slot._slotInventory = this;
            slots[index] = slot;
            index++;
        }
    }
    
}
