using System.Collections.Generic;

public class PlayerDataUnit
{
    public string unitName;
    public int unitLevel;

    public int unitPower;
    public int unitSpeed;

    public float maxHP;
    public float currentHP;
    public int unitIntelligence;
    public float currentMP;

    public int totalGold;

    public int currentExp;
    public int expToLevel;
    public int availableStatPoints;
    public int availableSkillPoints;

    public List<Skill> knownSkills = new List<Skill>();
    public List<Skill> equippedSkills = new List<Skill>();
}
