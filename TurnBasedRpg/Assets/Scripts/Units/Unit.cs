using System;
using System.Collections.Generic;
using UnityEngine;




public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int unitPower;
    public int unitSpeed;

    public float maxHP;
    public float currentHP;
    public float maxMP;
    public float currentMP;

    
    public List<Skill> equippedSkills = new List<Skill>();

    #region Combat
    public bool TakePhysicalDamage(float amountOfDamage)
    {
        //TODO: add defense
        currentHP -= amountOfDamage;

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

        if(currentMP >= maxMP)
        {
            currentMP = maxMP;
        }
    }

    public bool TakeMagicDamage(float amountOfDamage)
    {
        currentHP -= amountOfDamage;

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
