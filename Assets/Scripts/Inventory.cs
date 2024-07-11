using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Slot[] slots = new Slot[4];   
    public Slot[] Slots { get { return slots; } set { slots = value; } }
       

    private void Start()
    {
        BindingInventoryItemData();
        PutChildsInSlots();
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager._mousePosInventory = this; 
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager._mousePosInventory = null; 
    }


    private static void BindingInventoryItemData()
    {
        ConsumeInventoryItemData consumeInventoryItemData = new ConsumeInventoryItemData();
        ConsumeInventoryItemData consumeInventoryItemData2 = new ConsumeInventoryItemData();
        EquipInventoryItemData equipInventoryItemData = new EquipInventoryItemData();
        EquipInventoryItemData equipInventoryItemData2 = new EquipInventoryItemData();

        consumeInventoryItemData._itemID = "ȸ������ 001";
        consumeInventoryItemData2._itemID = "�������� 004";
        equipInventoryItemData._itemID = "������� 001";
        equipInventoryItemData2._itemID = "�ü���� 002";

        InventoryManager._inventoryItemDatas.Add(consumeInventoryItemData);
        InventoryManager._inventoryItemDatas.Add(consumeInventoryItemData2);
        InventoryManager._inventoryItemDatas.Add(equipInventoryItemData);
        InventoryManager._inventoryItemDatas.Add(equipInventoryItemData2);
    }


    // �� �κ��丮 �ڽĿ� �ִ� ��� ���Ե���, slots �迭�� �ְ�, �κ��丮�� ��� �Ҵ�
    private void PutChildsInSlots()
    {
        int index = 0;
        foreach (Slot slot in GetComponentsInChildren<Slot>())
        {
            slot._slotInventory = this;
            slot._slotItem = slot.transform.GetComponentInChildren<Item>();
            if (slot._slotItem != null)
            {
                slot._slotItem._itemInventory = this;
                slot._slotItem._itemNameText = slot._slotItem.GetComponentInChildren<TextMeshProUGUI>();
                slot._slotItem._inventoryItemData = 
                    InventoryManager._inventoryItemDatas[Random.Range(0, InventoryManager._inventoryItemDatas.Count)];
                
                Debug.Log($"������ �̸��� : {slot._slotItem.gameObject.name}");
                slot._slotItem._inventoryItemData.ShowInfo(slot._slotItem._itemNameText);
            }
                
            slots[index] = slot;
            index++;
        }
    }    
}
