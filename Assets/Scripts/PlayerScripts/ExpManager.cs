using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExpManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expMultiplier = 1.2f; // Multiplier for the experience needed to level up
    public Slider expSlider;
    public TMP_Text currentLevelText;

    public static event Action<int> OnLevelUp;

    public void Start()
    {

        UpdateUI();
    }
    public void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += GainExperience;
    }

      public void OnDisable()
    {
        Enemy_Health.OnMonsterDefeated -= GainExperience;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
        UpdateUI();
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevel)
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expMultiplier);
        OnLevelUp?.Invoke(1);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level.ToString();
    }
}
