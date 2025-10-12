using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform ItemsParent;
    public Transform ImportantItemParent;
    public GameObject InventorySlotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        InventoryManager inventoryManager = InventoryManager.Instance;

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager.Instance ¾øÀ½");
            return;
        }

        foreach (Transform child in ItemsParent) Destroy(child.gameObject);

        foreach (Transform child in ImportantItemParent)
        {
            if (child.GetComponent<InventorySlot>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Item item in inventoryManager.Items) 
        {
            GameObject slotGO = Instantiate(InventorySlotPrefab);

            if (item.IsImportant) slotGO.transform.SetParent(ImportantItemParent);
            else slotGO.transform.SetParent(ItemsParent);

            InventorySlot slot = slotGO.GetComponent<InventorySlot>();

            if (slot != null) slot.SetItem(item);

            slotGO.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}