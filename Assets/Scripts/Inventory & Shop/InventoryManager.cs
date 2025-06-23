using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InverntorySlot[] itemSlots;
    public UseItem useItem;
    public int gold;
    public TMP_Text goldText;
    public GameObject lootPrefab;

    public Transform player;

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
        Debug.Log("add item into slot");
        // Add the item to the inventory
        if (itemSO && (itemSO.isGold))
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }
        foreach (var slot in itemSlots)
        {
            if (slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
            {
                int availableSpace = itemSO.stackSize - slot.quantity;
                int amountToAdd = Mathf.Min(availableSpace, quantity);
                slot.quantity += amountToAdd;
                quantity -= amountToAdd;
                slot.UpdateUI();
                if (quantity <= 0)
                {
                    return;
                }
            }
        }
        // else
        // {
        foreach (var slot in itemSlots) //if item exist, we need look for empty slot
        {
            if (slot.itemSO == null)
            {
                int amountToAdd = Mathf.Min(itemSO.stackSize - quantity);
                Debug.Log("update item into slot");
                slot.itemSO = itemSO;
                slot.quantity = quantity;
                slot.UpdateUI();
                return;
            }

        }
        if (quantity > 0)
        {
            DropLoot(itemSO, quantity);
        }
            //UpdateUI();
        //}
    }
    private void DropLoot(ItemSO itemSO, int quanity)
    {
        Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO,quanity);
    }

    private void UpdateUI()
    {

    }

    public void UseItem(InverntorySlot slot)
    {
        if (slot.itemSO != null && slot.quantity >= 0)
        {
            Debug.Log("Trying to use item:" + slot.itemSO.itemName);
            useItem.ApplyItemEffects(slot.itemSO);
            slot.quantity--;
            if (slot.quantity <= 0)
            {
                slot.itemSO = null;
            }
            slot.UpdateUI();
        }
    }


}
