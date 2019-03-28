
using System;
using System.Collections.Generic;

public class InventoryManager : Singleton<InventoryManager>
{

    public int spaceSize = 20;

    public List<Item> items;
    public Action OnInventoryItemsChanged;
    public InventoryUI ui;

    public void Start(){
        ui.init();
        ui.inventory = this;
        OnInventoryItemsChanged += ui.UpdateUI;
    }
    
    private void Update()
    {
        if (GlobalManager.Instance.globalInput.menu)
        {
            ui.gameObject.SetActive(!ui.gameObject.activeSelf);
        }
    }

    internal bool Add(Item item)
    {
        if (items.Count <= spaceSize)
        {
            items.Add(item);
            OnInventoryItemsChanged?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        OnInventoryItemsChanged?.Invoke();
    }

}