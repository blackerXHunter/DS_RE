using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public GameObject inventorySlotsParent;
    public InventorySlot[] slots;
    public InventoryManager inventory;
    public GameObject inventoryUI;

    // Use this for initialization
    public void init()
    {
        slots = GetComponentsInChildren<InventorySlot>();
    }


    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].SetItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
