using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public Text playerNameText;
    public Text levelText;
    public Text healthText;
    public Text powerText;
    public Text manaText;
    public Text speedText;
    public Text statPointsText;

    public PlayerDataUnit player = PlayerDataTransfer.LoadPlayerData();

    public void UpdateTextFields()
    {
        playerNameText.text = PlayerDataTransfer.UnitName;
        levelText.text = PlayerDataTransfer.UnitLevel.ToString();
        healthText.text = PlayerDataTransfer.MaximumHP.ToString();
        powerText.text = PlayerDataTransfer.UnitPower.ToString();
        manaText.text = PlayerDataTransfer.UnitIntelligence.ToString();
        speedText.text = PlayerDataTransfer.UnitSpeed.ToString();
        statPointsText.text = $"Stat Points: {PlayerDataTransfer.AvailableStatPoints}";
    }

    public void IncreaseHP_Button()
    {
        if(player.availableStatPoints <= 0)
        {
            return;
        }
        else
        {
            player.maxHP += 10;
            player.availableStatPoints--;

            SavePlayerData();
            UpdateTextFields();
        }
    }

    public void IncreasePower_Button()
    {
        if(player.availableStatPoints <= 0)
        {
            return;
        }
        else
        {
            player.unitPower += 1;
            player.availableStatPoints--;

            SavePlayerData();
            UpdateTextFields();
        }
    }

    public void IncreaseMP_Button()
    {
        if(player.availableStatPoints <= 0)
        {
            return;
        }
        else
        {
            player.unitIntelligence += 3;
            player.availableStatPoints--;

            SavePlayerData();
            UpdateTextFields();
        }
    }

    public void IncreaseSpeed_Button()
    {
        if(player.availableStatPoints <= 0)
        {
            return;
        }
        else
        {
            player.unitSpeed += 1;
            player.availableStatPoints--;

            SavePlayerData();
            UpdateTextFields();
        }
    }

    public void ClosePanel()
    {
        //save player data on close.
        SavePlayerData();

        this.gameObject.SetActive(false);

        TownManager townManager = FindObjectOfType<TownManager>();
        townManager.megaBossButton.gameObject.SetActive(true);
        townManager.normalBossButton.gameObject.SetActive(true);
    }

    private void SavePlayerData()
    {
        PlayerDataTransfer.SavePlayerData(player);
    }
}
