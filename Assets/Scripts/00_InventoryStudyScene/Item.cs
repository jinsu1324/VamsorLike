using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Item : MonoBehaviour
{
    private Inventory _itemInventory;
    public Inventory ItemInventory { get { return _itemInventory; } set { _itemInventory = value; } }

    private InventoryItemData _inventoryItemData;
    public InventoryItemData InventoryItemData { get { return _inventoryItemData; } set { _inventoryItemData = value; } }

    private TextMeshProUGUI _itemNameText;
    public TextMeshProUGUI ItemNameText { get { return _itemNameText; } set { _itemNameText = value; } }
}
