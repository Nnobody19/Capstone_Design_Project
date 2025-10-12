using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Item> Items = new List<Item>();
    public int InventorySize = 12;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool Add(Item item)
    {
        if (Items.Count >= InventorySize) {
            return false;
        }

        Items.Add(item);
        Debug.Log(item.ItemName + "¿ª(∏¶) »πµÊ«ﬂ¥Ÿ.");
        return true;
    }

    private void Remove(Item item)
    {
        Items.Remove(item);
    }
}
