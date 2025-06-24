using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InverntorySlot : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    public ItemSO itemSO;
    public int quantity;

    public Image itemImage;//holds the icon that will show up when the slot is in use
    public TMP_Text quantityText;

    private InventoryManager inventoryManager;
    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();  
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if (quantity > 0)
        {

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (itemSO.currentHealth > 0 && StatsManager.Instance.currentHealth >= StatsManager.Instance.maxHealth)
                {
                    return;
                }
                inventoryManager.UseItem(this);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }
    }

    public void UpdateUI()
    {
        if (itemSO != null)
        {
            Debug.Log("item so is not null");
            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            Debug.Log("item so is null");
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }
}
