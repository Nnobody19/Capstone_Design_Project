using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{
    public Text ItemNameText;
    public Text ItemExplainText;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowToolTip(Item item)
    {
        if (item == null) return;

        ItemNameText.text = item.ItemName;
        ItemExplainText.text = item.ItemExplain;
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
