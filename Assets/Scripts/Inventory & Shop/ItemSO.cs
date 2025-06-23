using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    // Start is called before the first frame update
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite icon;
    public int stackSize = 3;

    public bool isGold;// the inventory will handle gold differently from other items, so this will notity the inventory whether it's gold or normal itemDescription

    [Header("Stats")]
    public int currentHealth;
    public int maxHealth;
    public int speed;
    public int damage;

    [Header("For Temporary Items")]
    public float duration; // how long the item lasts when used
}
