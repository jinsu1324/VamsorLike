using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public Slot[] SlotArr { get; set; }
       
    private void Start()
    {
        SlotArr = new Slot[4];

        BindingInventoryItemData();
        PutChildsInSlots();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager.MousePosInventory = this; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.MousePosInventory = null; 
    }

    private static void BindingInventoryItemData()
    {
        ConsumeInventoryItemData consumeInventoryItemData = new ConsumeInventoryItemData();
        ConsumeInventoryItemData consumeInventoryItemData2 = new ConsumeInventoryItemData();
        EquipInventoryItemData equipInventoryItemData = new EquipInventoryItemData();
        EquipInventoryItemData equipInventoryItemData2 = new EquipInventoryItemData();

        consumeInventoryItemData.ItemID = "ȸ������ 001";
        consumeInventoryItemData2.ItemID = "�������� 004";
        equipInventoryItemData.ItemID = "������� 001";
        equipInventoryItemData2.ItemID = "�ü���� 002";

        InventoryManager.InventoryItemDataList.Add(consumeInventoryItemData);
        InventoryManager.InventoryItemDataList.Add(consumeInventoryItemData2);
        InventoryManager.InventoryItemDataList.Add(equipInventoryItemData);
        InventoryManager.InventoryItemDataList.Add(equipInventoryItemData2);
    }

    // �� �κ��丮 �ڽĿ� �ִ� ��� ���Ե���, slots �迭�� �ְ�, �κ��丮�� ��� �Ҵ�
    private void PutChildsInSlots()
    {
        int index = 0;
        foreach (Slot slot in GetComponentsInChildren<Slot>())
        {
            slot.SlotInventory = this;
            slot.SlotItem = slot.transform.GetComponentInChildren<Item>();
            if (slot.SlotItem != null)
            {
                slot.SlotItem.ItemInventory = this;
                slot.SlotItem.ItemNameText = slot.SlotItem.GetComponentInChildren<TextMeshProUGUI>();
                slot.SlotItem.InventoryItemData = 
                    InventoryManager.InventoryItemDataList[Random.Range(0, InventoryManager.InventoryItemDataList.Count)];
                
                Debug.Log($"������ �̸��� : {slot.SlotItem.gameObject.name}");
                slot.SlotItem.InventoryItemData.ShowInfo(slot.SlotItem.ItemNameText);
            }

            SlotArr[index] = slot;
            index++;
        }
    }    
}
