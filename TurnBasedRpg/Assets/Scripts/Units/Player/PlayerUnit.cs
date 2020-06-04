using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public event EventHandler<int> OnLevelUp;

    public int totalGold;
    public int currentExp;
    public int expToLevel;
    public int availableStatPoints;
    public int availableSkillPoints;

    public List<Skill> knownSkills = new List<Skill>();

    public void GainGold(int amount)
    {
        totalGold += amount;
        Debug.Log($"Gained {amount} gold.");
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;

        Debug.Log($"Gained {amount} exp.");

        if (currentExp >= expToLevel)
        {
            int balance = currentExp - expToLevel;
            LevelUp();
            currentExp = balance;
        }
    }

    public void LevelUp()
    {
        unitLevel++;
        availableStatPoints++;
        availableSkillPoints++;
        expToLevel = unitLevel * 50;
        OnLevelUp?.Invoke(this, 1000);

        Debug.Log("Player level up");
    }
}
