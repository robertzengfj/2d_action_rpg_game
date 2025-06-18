using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InverntorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemSO itemSO;
    public int quantity;

    public Image itemImage;//holds the icon that will show up when the slot is in use
    public TMP_Text quantityText;
    public void UpdateUI()
    {
        if (itemSO != null)
        {

            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }
}
