using System.Collections.Generic;

public class PlayerDataUnit
{
    public string unitName;
    public int unitLevel;

    public int unitPower;
    public int unitSpeed;

    public float maxHP;
    public float currentHP;
    public float maxMP;
    public float currentMP;

    public int totalGold;

    public float currentExp;
    public float expToLevel;
    public int availableStatPoints;
    public int availableSkillPoints;

    public List<Skill> knownSkills = new List<Skill>();
    public List<Skill> equippedSkills = new List<Skill>();
}
