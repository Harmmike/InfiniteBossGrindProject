
using System.Collections.Generic;
using System.Linq;

public static class SkillFactory
{
    public static List<Skill> GameSkills = new List<Skill>();

    static SkillFactory()
    {
        LoadSkills();
    }

    private static void LoadSkills()
    {
        GameSkills.Add(new Skill(9001, "Heavy Attack", TypeOfSkill.Physical, 2f, 2, 0, EffectOfSkill.ATTACKHP));
        GameSkills.Add(new Skill(9002, "Debilitating Hit", TypeOfSkill.Physical, 0.2f, 3, 2, EffectOfSkill.ATTACKMP, EffectOfSkill.ATTACKHP));
        GameSkills.Add(new Skill(9003, "Reserved Swing", TypeOfSkill.Physical, 0.5f, 0, 0.5f, EffectOfSkill.ATTACKHP, EffectOfSkill.GAINMP));

        GameSkills.Add(new Skill(9004, "Vampiric Slash", TypeOfSkill.Physical, 1.2f, 2, 0.5f, EffectOfSkill.ATTACKHP, EffectOfSkill.GAINHP));
        GameSkills.Add(new Skill(9005, "Heal", TypeOfSkill.Magic, 0f, 2, 2f, EffectOfSkill.GAINHP));
        GameSkills.Add(new Skill(9006, "Fireball", TypeOfSkill.Magic, 2f, 2, 0f, EffectOfSkill.ATTACKHP));
        GameSkills.Add(new Skill(9007, "Ethereal Spirit", TypeOfSkill.Magic, 1.2f, 2, 0.5f, EffectOfSkill.ATTACKHP, EffectOfSkill.GAINHP));
        //GameSkills.Add(new Skill(9008, "Invigoration", TypeOfSkill.Magic, 0.5f, 0, 0.5f, EffectOfSkill.ATTACKHP, EffectOfSkill.GAINMP));
        GameSkills.Add(new Skill(9009, "Enfeebling Incantation", TypeOfSkill.Magic, 0.5f, 2, 0.5f, EffectOfSkill.ATTACKHP, EffectOfSkill.ATTACKMP));
        GameSkills.Add(new Skill(9010, "Mega Slam", TypeOfSkill.Physical, 4f, 5, 0f, EffectOfSkill.ATTACKHP));
        GameSkills.Add(new Skill(9011, "Overwhelm", TypeOfSkill.Magic, 6f, 5, 0f, EffectOfSkill.ATTACKHP));
        GameSkills.Add(new Skill(9012, "Impulse Surge", TypeOfSkill.Magic, 2f, 6, 2f, EffectOfSkill.ATTACKHP, EffectOfSkill.ATTACKMP));
        GameSkills.Add(new Skill(9013, "Charged Strike", TypeOfSkill.Physical, 10f, 10, 0f, EffectOfSkill.ATTACKHP));
    }

    public static Skill GetSkillByID(int id)
    {
        return GameSkills.FirstOrDefault(s => s.SkillID == id)?.Clone();
    }
}
