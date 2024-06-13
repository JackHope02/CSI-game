using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int itemType;  // Differentiate items (1 = Clue Duster, 2 = Evidence Bag, 3 = Flashlight)

    public Item(string name, Sprite iconSprite, int type)
    {
        itemName = name;
        icon = iconSprite;
        itemType = type;
    }
}