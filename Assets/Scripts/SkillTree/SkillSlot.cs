using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class SkillSlot : MonoBehaviour
{
    public List<SkillSlot> prerequisiteSkillSlots;
    public SkillSO skillSO;
    public int currentLevel;
    public bool isUnlocked;
    public Image skillIcon;
    public Button skillButton;
    public TMP_Text skillLevelText;

    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;
    private void OnValidate()
    {
        if (skillSO != null && skillLevelText != null)
        {

            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;
        if (isUnlocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.grey;
        }
    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this);
            if (currentLevel >= skillSO.maxLevel)
            {
                //isUnlocked = false; // Disable further upgrades
                OnSkillMaxed?.Invoke(this);
            }
            UpdateUI();
        }

    }
    public bool CanUnlockSkill()
    {
        foreach(SkillSlot slot in prerequisiteSkillSlots)
        {
            if (!slot.isUnlocked||slot.currentLevel<slot.skillSO.maxLevel)
            {
                return false; // Cannot unlock if any prerequisite skill is not unlocked
            }
        }
        // Check if the skill can be unlocked based on some conditions, e.g., player level or other skills
        // For simplicity, let's assume it can be unlocked if the current level is 0
        return true;
    }

    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }

}
