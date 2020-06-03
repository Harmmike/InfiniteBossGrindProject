using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class WebParser : MonoBehaviour
{
    public PlayerDataUnit ParsePlayerData(string inputData)
    {
        string[] parsedData = inputData.Split(',');
        List<string> kSkills = new List<string>();
        List<string> eSkills = new List<string>();

        if (!string.IsNullOrEmpty(parsedData[10]))
        {
            string[] skills = parsedData[10].Split('-');

            foreach (var skill in skills)
            {
                kSkills.Add(skill);
            }
        }

        if (!string.IsNullOrEmpty(parsedData[11]))
        {
            string[] skills = parsedData[11].Split('-');

            foreach (var skill in skills)
            {
                eSkills.Add(skill);
            }
        }
         

        List<int> knownSkills = new List<int>();
        List<int> equippedSkills = new List<int>();

        for (int i = 0; i < kSkills.Count; i++)
        {
            int skillId = int.Parse(kSkills[i]);
            knownSkills.Add(skillId);
        }

        for (int i = 0; i < eSkills.Count; i++)
        {
            int skillId = int.Parse(eSkills[i]);
            equippedSkills.Add(skillId);
        }

        PlayerDataUnit player = new PlayerDataUnit();

        player.unitName = parsedData[0];
        player.unitLevel = int.Parse(parsedData[1]);
        player.unitPower = int.Parse(parsedData[2]);
        player.unitSpeed = int.Parse(parsedData[3]);
        player.unitIntelligence = int.Parse(parsedData[4]);
        player.maxHP = float.Parse(parsedData[5]);
        player.totalGold = 0;
        player.currentExp = 0;
        player.availableSkillPoints = 0;
        player.availableStatPoints = 0;

        foreach (var skillId in knownSkills)
        {
            player.knownSkills.Add(SkillFactory.GetSkillByID(skillId));
        }

        foreach (var skillId in equippedSkills)
        {
            player.equippedSkills.Add(SkillFactory.GetSkillByID(skillId));
        }

        return player;
    }

    public Dictionary<string, int> ParseLeaderboardData(string inputData)
    {

        Dictionary<string, int> players = new Dictionary<string, int>();

        string[] parsedPlayers = inputData.Split('/');

        for (int i = 0; i < parsedPlayers.Length; i++)
        {
            string[] player = parsedPlayers[i].Split(',');

            if (string.IsNullOrEmpty(player[0]))
            {
                return players;
            }

            Debug.Log($"{player[0]} {player[1]}");

            string username = player[0];
            int level = int.Parse(player[1].ToString());

            players.Add(username, level);
        }

        return players;
    }
}
