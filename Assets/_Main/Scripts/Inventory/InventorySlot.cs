using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Item item;
    public Image iconImage;
    public Button useButton;
    public Button removeButton;

    internal void SetItem(Item newItem)
    {
        item = newItem;
        iconImage.sprite = item.icon;
        iconImage.enabled = true;
        useButton.interactable = true;
        removeButton.gameObject.SetActive(true);
        
    }

    public void ClearSlot()
    {
        item = null;
        iconImage.enabled = false;
        iconImage.sprite = null;
        useButton.interactable = false;
        removeButton.gameObject.SetActive(false);
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.Remove(item);
    }

    public void UseItem()
    {
        item.Use();
    }
}
