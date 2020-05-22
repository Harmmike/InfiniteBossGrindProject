using System.Collections.Generic;

public static class PlayerDataTransfer
{
    public static string UnitName { get; set; }
    public static int UnitLevel { get; set; }
    public static int UnitPower { get; set; }
    public static int UnitSpeed { get; set; }
    public static float MaximumHP { get; set; }
    public static float MaximumMP { get; set; }
    public static int TotalGold { get; set; }
    public static float CurrentExp { get; set; }
    public static int AvailableStatPoints { get; set; }
    public static int AvailableSkillPoints { get; set; }

    public static List<Skill> KnownSkills = new List<Skill>();
    public static List<Skill> EquippedSkills = new List<Skill>();

    public static bool SavePlayerData(PlayerUnit playerData)
    {
        bool saveSuccessful;

        if(playerData != null)
        {
            UnitName = playerData.unitName;
            UnitLevel = playerData.unitLevel;
            UnitPower = playerData.unitPower;
            UnitSpeed = playerData.unitSpeed;
            MaximumHP = playerData.maxHP;
            MaximumMP = playerData.maxMP;
            CurrentExp = playerData.currentExp;
            TotalGold = playerData.totalGold;
            AvailableStatPoints = playerData.availableStatPoints;
            AvailableSkillPoints = playerData.availableSkillPoints;

            KnownSkills = playerData.knownSkills;
            EquippedSkills = playerData.equippedSkills;

            saveSuccessful = true;
        }
        else
        {
            saveSuccessful = false;
        }
        return saveSuccessful;
    }

    public static bool SavePlayerData(PlayerDataUnit playerData)
    {
        bool saveSuccessful;

        if(playerData != null)
        {
            UnitName = playerData.unitName;
            UnitLevel = playerData.unitLevel;
            UnitPower = playerData.unitPower;
            UnitSpeed = playerData.unitSpeed;
            MaximumHP = playerData.maxHP;
            MaximumMP = playerData.maxMP;
            CurrentExp = playerData.currentExp;
            TotalGold = playerData.totalGold;
            AvailableStatPoints = playerData.availableStatPoints;
            AvailableSkillPoints = playerData.availableSkillPoints;

            KnownSkills = playerData.knownSkills;
            EquippedSkills = playerData.equippedSkills;

            saveSuccessful = true;
        }
        else
        {
            saveSuccessful = false;
        }
        return saveSuccessful;
    }

    public static PlayerDataUnit LoadPlayerData()
    {
        PlayerDataUnit newUnit = new PlayerDataUnit();
        newUnit.unitName = UnitName;
        newUnit.unitLevel = UnitLevel;
        newUnit.unitPower = UnitPower;
        newUnit.unitSpeed = UnitSpeed;
        newUnit.maxHP = MaximumHP;
        newUnit.currentHP = MaximumHP;
        newUnit.maxMP = MaximumMP;
        newUnit.currentMP = 0;
        newUnit.currentExp = CurrentExp;
        newUnit.expToLevel = UnitLevel * 50;
        newUnit.totalGold = TotalGold;
        newUnit.availableStatPoints = AvailableStatPoints;
        newUnit.availableSkillPoints = AvailableSkillPoints;

        newUnit.knownSkills = KnownSkills;
        newUnit.equippedSkills = EquippedSkills;

        return newUnit;
    }
}
