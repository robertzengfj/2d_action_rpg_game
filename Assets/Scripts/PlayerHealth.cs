using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;

    public Animator healthTextAnim;

    private void Start()
    {
        
        healthText.text = "HP: " + currentHealth.ToString()+" / "+maxHealth.ToString();
    }

    public void ChangeHealth(int amount){
        currentHealth+= amount;
        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + currentHealth.ToString()+" / "+maxHealth.ToString();
        if(currentHealth<=0){
            gameObject.SetActive(false);
        }
    }
}
