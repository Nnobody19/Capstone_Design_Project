using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Consumable, Miscellaneous, Important }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    public string ItemName = "새 아이템";
    public Sprite ItemIcon = null;
    public bool IsImportant;
    public ItemType itemType;

    public virtual void Use()
    {

    }
}
