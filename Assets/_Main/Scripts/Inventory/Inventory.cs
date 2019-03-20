
using System;
using System.Collections.Generic;

public class Inventory : Singleton<Inventory>
{

    public int spaceSize = 20;

    public List<Item> items;
    internal Action onInventoryItemsChanged;

    internal bool Add(Item item)
    {
        if (items.Count <= spaceSize)
        {
            items.Add(item);
            onInventoryItemsChanged?.Invoke();
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
        onInventoryItemsChanged?.Invoke();
    }

}