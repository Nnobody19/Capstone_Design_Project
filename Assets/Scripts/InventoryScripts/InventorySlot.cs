using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image Icon;

    public void SetItem(Item item)
    {
        if (item != null && item.ItemIcon != null)
        {
            Icon.sprite = item.ItemIcon;
            Icon.enabled = true;
        }

        else
        {
            Icon.sprite = null;
            Icon.enabled = false;
        }
    }
}
