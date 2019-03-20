using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="new Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("Using Item :" + name);
        RemoveItem();
    }

    public void RemoveItem()
    {
        Inventory.Instance.Remove(this);
    }

}