
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
    }

    public static Skill GetSkillByID(int id)
    {
        return GameSkills.FirstOrDefault(s => s.SkillID == id)?.Clone();
    }
}
