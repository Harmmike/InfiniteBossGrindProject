using Assets.Scripts.IO;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveScript
{
    private static string filePath = Application.persistentDataPath + "/playersave.save";

    public static void SaveData(PlayerDataUnit player)
    {
            var save = new SaveData()
            {
                SavedName = player.unitName,
                SavedLevel = player.unitLevel,
                SavedHP = player.maxHP,
                SavedMP = player.maxMP,
                SavedPower = player.unitPower,
                SavedSpeed = player.unitSpeed,
                SavedCurrentXP = player.currentExp,
                SavedXpToLvl = player.expToLevel,
                SavedGold = player.totalGold,
                SavedStatPoints = player.availableStatPoints,
                SavedEquippedSkills = player.equippedSkills,
                SavedKnownSkills = player.knownSkills
            };

            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = File.Create(filePath))
            {
                binaryFormatter.Serialize(fileStream, save);
            }

            Debug.Log("Data saved");        
    }

    public static bool LoadData()
    {
        if (File.Exists(filePath))
        {
            SaveData save;
            PlayerDataUnit newPlayer = new PlayerDataUnit();

            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = File.Open(filePath, FileMode.Open))
            {
                save = (SaveData)binaryFormatter.Deserialize(fileStream);
            }

            newPlayer.unitName = save.SavedName;
            newPlayer.unitLevel = save.SavedLevel;
            newPlayer.maxHP = save.SavedHP;
            newPlayer.currentHP = save.SavedHP;
            newPlayer.maxMP = save.SavedMP;
            newPlayer.currentMP = save.SavedMP;
            newPlayer.unitSpeed = save.SavedSpeed;
            newPlayer.availableStatPoints = save.SavedStatPoints;
            newPlayer.unitPower = save.SavedPower;
            newPlayer.expToLevel = save.SavedXpToLvl;
            newPlayer.availableSkillPoints = save.SavedSkillPoints;
            newPlayer.knownSkills = save.SavedKnownSkills;
            newPlayer.equippedSkills = save.SavedEquippedSkills;

            PlayerDataTransfer.SavePlayerData(newPlayer);

            Debug.Log("Data Loaded");
            return true;
        }
        else
        {
            Debug.LogWarning("Save file doesn't exist");
            return false;
        }
    }
}
