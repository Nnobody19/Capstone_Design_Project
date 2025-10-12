using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    public string ItemName = "새 아이템";
    public Sprite ItemIcon = null;
    public bool IsImportant = false;        // 사용 불가능한 아이템인지 판별
}
