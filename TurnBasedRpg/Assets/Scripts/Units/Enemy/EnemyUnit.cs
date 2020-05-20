
public enum DynamicStats
{
    POWER,
    HP,
    MP
}

public class EnemyUnit : Unit
{
    public DynamicStats dynamicStatOne;
    public DynamicStats dynamicStatTwo;

    public int rewardGold;
    public int rewardExp;

    public void SetBossStats()
    {

       int statWeight = unitLevel / 2;

       switch (dynamicStatOne)
       {
           case DynamicStats.HP:
               maxHP += statWeight * 10;
               currentHP = maxHP;
               break;

           case DynamicStats.MP:
               maxMP += statWeight * 5;
               break;

           case DynamicStats.POWER:
               unitPower += statWeight * 2;
               break;
       }

       switch (dynamicStatTwo)
       {
           case DynamicStats.HP:
               maxHP += statWeight * 10;
               currentHP = maxHP;
               break;

           case DynamicStats.MP:
               maxMP += statWeight * 5;
               break;

           case DynamicStats.POWER:
               unitPower += statWeight * 2;
               break;
       }
    }

    public void SetBossSkills()
    {
        switch (dynamicStatOne)
        {
            case DynamicStats.HP:
                equippedSkills.Add(SkillFactory.GetSkillByID(9002));
                break;

            case DynamicStats.MP:
                equippedSkills.Add(SkillFactory.GetSkillByID(9003));
                break;

            case DynamicStats.POWER:
                equippedSkills.Add(SkillFactory.GetSkillByID(9001));
                break;
        }
    }
}
