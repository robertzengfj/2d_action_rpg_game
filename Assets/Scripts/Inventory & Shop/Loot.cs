using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer sr;
    public Animator anim;

    public bool canBePickedUp = true;
    public static event Action<ItemSO, int> OnItemLooted;
    public int quantity;
    //this get called anytime you make changes in the inspector
    private void OnValidate()
    {
        if (itemSO == null)
        {
            return;
        }

        this.UpdateAppearance();
    }
    public void Initialize(ItemSO itemSO, int quantity)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;
        canBePickedUp = false;
        this.UpdateAppearance();
    }
    public void UpdateAppearance()
    {
        sr.sprite = itemSO.icon;
        this.name = itemSO.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canBePickedUp == true)
        {
            anim.Play("LootPickup");
            OnItemLooted?.Invoke(itemSO, quantity);
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canBePickedUp == false)
        {
            canBePickedUp = true;
        }
    }
}
