using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void Start()
    {
        // �κ��丮 �ڽĵ� �����κ��丮�� ���ε�
        foreach (Item item in GetComponentsInChildren<Item>())
            item._invenState = this;
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
