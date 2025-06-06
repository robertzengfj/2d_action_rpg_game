using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // public int currentHealth;
    // public int maxHealth;

    public TMP_Text healthText;

    public Animator healthTextAnim;

    private void Start()
    {
        
        healthText.text = "HP: " + StatsManager.Instance.currentHealth.ToString()+" / "+StatsManager.Instance.maxHealth.ToString();
    }

    public void ChangeHealth(int amount){
        StatsManager.Instance.currentHealth+= amount;
        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + StatsManager.Instance.currentHealth.ToString()+" / "+StatsManager.Instance.maxHealth.ToString();
        if(StatsManager.Instance.currentHealth<=0){
            Debug.Log("Player is dead");
            gameObject.SetActive(false);
        }
    }
}
