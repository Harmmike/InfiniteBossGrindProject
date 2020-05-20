using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public GameObject mainButtonPanel;
    public GameObject newGamePanel;

    #region Buttons
    public void NewGame_Button()
    {
        mainButtonPanel.SetActive(false);
        newGamePanel.SetActive(true);
    }

    public void LoadGame_Button()
    {
        if (SaveScript.LoadData())
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            return;
        }
    }

    public void Menu_Button()
    {
        Debug.Log("Open menu");
    }
    #endregion

    #region Actions
    public void GoToTitleScreen()
    {
        newGamePanel.SetActive(false);
        mainButtonPanel.SetActive(true);
    }

    public void BeginNewGame(string playerName)
    {
        Debug.Log($"{playerName} is starting a new game.");

        CreateNewPlayer(playerName);

        SceneManager.LoadScene(1);
    }
    #endregion

    private void CreateNewPlayer(string playerName)
    {
        PlayerDataUnit newPlayer = new PlayerDataUnit();

        newPlayer.unitName = playerName;
        newPlayer.unitLevel = 1;
        newPlayer.unitPower = 10;
        newPlayer.maxHP = 50;
        newPlayer.maxMP = 10;
        newPlayer.unitSpeed = 2;
        newPlayer.expToLevel = 50;
        newPlayer.currentExp = 0;
        newPlayer.availableStatPoints = 0;
        newPlayer.availableSkillPoints = 0;

        PlayerDataTransfer.SavePlayerData(newPlayer);
    }
}
