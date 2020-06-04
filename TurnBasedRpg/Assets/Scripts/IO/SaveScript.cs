using Assets.Scripts.IO;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public static class SaveScript
{
    private static readonly string filePath = Application.persistentDataPath + "/playersave.save";
    private static readonly string SAVEPLAYER_URL = "https://harmsoft.000webhostapp.com/saveplayer.php";

    public static void SaveData(PlayerDataUnit player)
    {
            var save = new SaveData()
            {
                SavedName = player.unitName,
                SavedLevel = player.unitLevel,
                SavedHP = player.maxHP,
                SavedIntelligence = player.unitIntelligence,
                SavedPower = player.unitPower,
                SavedDex = player.unitSpeed,
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
            newPlayer.unitIntelligence = save.SavedIntelligence;
            newPlayer.currentMP = save.SavedIntelligence;
            newPlayer.unitSpeed = save.SavedDex;
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

    public static IEnumerator OnlineSaveData(PlayerDataUnit player)
    {
        Debug.Log("starting OnlineSaveData()");

        WWWForm form = new WWWForm();
        form.AddField("username", player.unitName);
        form.AddField("level", player.unitLevel);
        form.AddField("power", player.unitPower);
        form.AddField("speed", player.unitSpeed);
        form.AddField("maxhp", player.maxHP.ToString());
        form.AddField("intelligence", player.unitIntelligence);
        form.AddField("gold", player.totalGold);
        form.AddField("exp", player.currentExp);
        form.AddField("statpoints", player.availableStatPoints);
        form.AddField("skillpoints", player.availableSkillPoints);

        using (UnityWebRequest request = UnityWebRequest.Post(SAVEPLAYER_URL, form))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }

        Debug.Log("finished OnlineSaveData()");
    }
}
