using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InverntorySlot[] itemSlots;
    public int gold;
    public TMP_Text goldText;

    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUI();
        }
    }
    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    private void AddItem(ItemSO itemSO, int quantity)
    {
        // Add the item to the inventory
        if (itemSO.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }
        else
        {
            foreach (var slot in itemSlots)
            {
                if (slot.itemSO == null)
                {
                    slot.itemSO = itemSO;
                    slot.quantity = quantity;
                    slot.UpdateUI();
                    return;
                }
                
            }
            UpdateUI();
        }
    }

    private void UpdateUI()
    {

    }


}
