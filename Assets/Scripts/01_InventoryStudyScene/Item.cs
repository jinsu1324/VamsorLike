using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Item : MonoBehaviour
{
    public Inventory ItemInventory { get; set; }

    public InventoryItemData InventoryItemData { get; set; } = new InventoryItemData();

    public TextMeshProUGUI ItemNameText { get; set; }
}
