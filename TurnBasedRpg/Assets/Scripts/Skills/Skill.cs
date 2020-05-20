using UnityEngine;

public enum TypeOfSkill
{
    Physical,
    Magic,
    True,
    Passive
}

public enum EffectOfSkill
{
    NONE,
    ATTACKHP,
    ATTACKMP,
    GAINHP,
    GAINMP,
    DOT
}

[System.Serializable]
public class Skill
{
    public int SkillID { get; set; }
    public string SkillName { get; set; }
    public TypeOfSkill SkillType { get; set; }
    public float DamageModifier { get; set; }
    public int EnergyCost { get; set; }
    public float SkillEffectModifier { get; set; }
    public EffectOfSkill SkillEffectOne { get; set; }
    public EffectOfSkill SkillEffectTwo { get; set; }
    public string SkillDescription { get; set; }

    public Skill(int skillID, string skillName, TypeOfSkill skillType, float damageModifier, int energyCost, float skillEffectModifier, EffectOfSkill skillEffectOne, EffectOfSkill skillEffectTwo = EffectOfSkill.NONE, string skillDescription = "No description available.")
    {
        SkillID = skillID;
        SkillName = skillName;
        SkillType = skillType;
        DamageModifier = damageModifier;
        EnergyCost = energyCost;
        SkillEffectOne = skillEffectOne;
        SkillEffectTwo = skillEffectTwo;
        SkillEffectModifier = skillEffectModifier;
        SkillDescription = skillDescription;
    }

    public Skill Clone()
    {
        return new Skill(SkillID, SkillName, SkillType, DamageModifier, EnergyCost, SkillEffectModifier, SkillEffectOne, SkillEffectTwo);
    }
}
