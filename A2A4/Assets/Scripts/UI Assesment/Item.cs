using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Consumable
}

[CreateAssetMenu(fileName = "newItem", menuName = "Create item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    [TextArea] public string desc;
    public ItemType itemType;
    public int worthValue;
}
