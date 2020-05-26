
using UnityEngine;

public enum DynamicStats
{
    POWER,
    HP,
    INTELLIGENCE,
    DEX
}

public class EnemyUnit : Unit
{
    public DynamicStats dynamicStatOne;
    public DynamicStats dynamicStatTwo;

    public int rewardGold;
    public int rewardExp;

    public bool isMegaBoss;

    private void SetDynamicStats()
    {
        int dynamicStatOneRng = Random.Range(0, 3);
        int dynamicStatTwoRng = Random.Range(0, 3);

        switch (dynamicStatOneRng)
        {
            case 0:
                dynamicStatOne = DynamicStats.POWER;
                break;

            case 1:
                dynamicStatOne = DynamicStats.INTELLIGENCE;
                break;

            case 2:
                dynamicStatOne = DynamicStats.HP;
                break;

            case 3:
                dynamicStatOne = DynamicStats.DEX;
                break;
        }

        switch (dynamicStatTwoRng)
        {
            case 0:
                dynamicStatTwo = DynamicStats.POWER;
                break;

            case 1:
                dynamicStatTwo = DynamicStats.INTELLIGENCE;
                break;

            case 2:
                dynamicStatTwo = DynamicStats.HP;
                break;

            case 3:
                dynamicStatTwo = DynamicStats.DEX;
                break;
        }
    }
    private void AllocateStatPoints()
    {
        //subtract 1 because we have base level 1 stats.
        int bossSkillPoints = unitLevel - 1;

        int statWeight = Mathf.RoundToInt(bossSkillPoints / 2);


        switch (dynamicStatOne)
        {
            case DynamicStats.HP:
                maxHP += statWeight * 10;
                currentHP = maxHP;
                break;
        }

        //for primary stat we're adding 1 to the floor'd int.
        switch (dynamicStatOne)
        {
            default:
                break;

            case DynamicStats.HP:
                maxHP += (statWeight + 1) * 10;
                currentHP = maxHP;
                break;

            case DynamicStats.INTELLIGENCE:
                unitIntelligence += (statWeight + 1) * 2;
                break;

            case DynamicStats.POWER:
                unitPower += (statWeight + 1) * 2;
                break;

            case DynamicStats.DEX:
                unitDex += statWeight + 1;
                break;
        }

        switch (dynamicStatTwo)
        {
            case DynamicStats.HP:
                maxHP += statWeight * 10;
                currentHP = maxHP;
                break;
        }

        switch (dynamicStatTwo)
        {
            default:
                break;

            case DynamicStats.HP:
                maxHP += statWeight * 10;
                currentHP = maxHP;
                break;

            case DynamicStats.INTELLIGENCE:
                unitIntelligence += statWeight * 5;
                break;

            case DynamicStats.POWER:
                unitPower += statWeight * 2;
                break;

            case DynamicStats.DEX:
                unitDex += statWeight;
                break;
        }
    }
    private void AdjustRewards()
    {
        rewardExp = unitLevel * 10;
        rewardGold = unitLevel * 10;
    }

    public void InitializeBossSettings()
    {
        SetDynamicStats();
        AllocateStatPoints();
        AdjustRewards();
        SetBossSkills();
    }

    private void SetBossSkills()
    {
        switch (dynamicStatOne)
        {
            case DynamicStats.HP:
                equippedSkills.Add(SkillFactory.GetSkillByID(9002));
                break;

            case DynamicStats.INTELLIGENCE:
                equippedSkills.Add(SkillFactory.GetSkillByID(9003));
                break;

            case DynamicStats.POWER:
                equippedSkills.Add(SkillFactory.GetSkillByID(9001));
                break;
        }
    }
}
