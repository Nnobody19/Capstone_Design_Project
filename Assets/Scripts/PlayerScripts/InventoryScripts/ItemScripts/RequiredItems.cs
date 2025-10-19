using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ChapterRequirements, menuName = Inventory/Chapter Requirements")]
public class RequiredItems : ScriptableObject 
{
    public int ChapterNumber;
    public List<Item> RequireItems;
}
