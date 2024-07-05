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
        // ���� �κ��丮�� ���Ե���, ���Թ迭�� �Ҵ�
        int index = 0;        
        foreach (Slot slot in GetComponentsInChildren<Slot>())
        {
            slot._invenState = this;
            slots[index] = slot;
            index++;
        }
            
    }

    // ���콺�� �κ��丮�� ������ �� 1ȸ ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager._invenState = this;
    }

    // ���콺�� �κ��丮���� ������ �� 1ȸ ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager._invenState = null;
    }

    
}
