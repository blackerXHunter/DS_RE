using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public GameObject inventorySlotsParent;
    public InventorySlot[] slots;
    public Inventory inventory;
    public GameObject inventoryUI;

    // Use this for initialization
    void Start()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        inventory = Inventory.Instance;
        inventory.onInventoryItemsChanged += UpdateUI;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    private void UpdateUI()
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
