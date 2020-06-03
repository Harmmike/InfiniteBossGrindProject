using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public GameObject mainButtonPanel;
    public GameObject newGamePanel;
    public GameObject mainOnlinePanel;

    public GameObject onlinePanel;
    public GameObject loginPanel;
    public GameObject registerPanel;

    public LoginScript loginController;

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

    public void OnlineGame_Button()
    {
        Debug.Log("online game button pressed");

        mainButtonPanel.SetActive(false);
        mainOnlinePanel.SetActive(true);
        onlinePanel.SetActive(true);

        //StartCoroutine(loginController.ProcessRequest("Test1", "testPassword"));
        //loginController.ProcessRequest("Test", "testPassword");
    }

    public void Menu_Button()
    {
        Debug.Log("Open menu");
    }
    #endregion

    #region Online Stuff
    public void OnlinePanel_Login_Button()
    {
        onlinePanel.SetActive(false);
        registerPanel.SetActive(false);

        loginPanel.SetActive(true);
    }

    public void OnlinePanel_Register_Button()
    {
        onlinePanel.SetActive(false);
        loginPanel.SetActive(false);

        registerPanel.SetActive(true);
    }

    public void OnlinePanel_Back_Button()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        mainOnlinePanel.SetActive(false);
        onlinePanel.SetActive(true);

        mainButtonPanel.SetActive(true);
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
        newPlayer.unitIntelligence = 10;
        newPlayer.unitSpeed = 2;
        newPlayer.expToLevel = 50;
        newPlayer.currentExp = 0;
        newPlayer.availableStatPoints = 0;
        newPlayer.availableSkillPoints = 0;

        PlayerDataTransfer.SavePlayerData(newPlayer);
    }
}
