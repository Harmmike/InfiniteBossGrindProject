using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int unitPower;
    public int unitIntelligence;
    public int unitDex;

    public float maxHP;
    public float currentHP;

    private float currentMP;

    public float MaxMP
    {
        get { return unitIntelligence * 0.5f; }
    }
    public float CurrentMP { 
        get 
        { return currentMP; }
    }

    public int PhysicalDefense
    {
        get
        {
            return (unitPower + unitDex) / 2;
        }
    }

    public int MagicDefense
    {
        get
        {
            return (unitIntelligence + unitDex) / 2;
        }
    }
    //public float maxMP;
    //public float currentMP;

    public List<Skill> equippedSkills = new List<Skill>();

    #region Combat
    public bool TakePhysicalDamage(float amountOfDamage)
    {
        //TODO: add defense
        var reducedDamage = amountOfDamage - PhysicalDefense;

        if(reducedDamage < 1)
        {
            reducedDamage = 1;
        }

        currentHP -= reducedDamage;

        return IsDead();
    }

    public void LoseEnergy(float amountOfEnergy)
    {
        currentMP -= amountOfEnergy;

        if(currentMP <= 0)
        {
            currentMP = 0;
        }
    }

    public void GainHitPoints(float amountOfHp)
    {
        currentHP += amountOfHp;

        if(currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void GainEnergy(float amountOfEnergy)
    {
        currentMP += amountOfEnergy;

        if(currentMP >= MaxMP)
        {
            currentMP = MaxMP;
        }
    }

    public bool TakeMagicDamage(float amountOfDamage)
    {
        var reducedDamage = amountOfDamage - MagicDefense;

        if(reducedDamage < 1)
        {
            reducedDamage = 1;
        }

        currentHP -= reducedDamage;

        return IsDead();
    }

    public bool IsDead()
    {
        if(currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
