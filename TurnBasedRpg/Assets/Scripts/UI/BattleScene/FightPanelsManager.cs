using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class FightPanelsManager : MonoBehaviour
{
    public GameObject levelUpIndicator;

    #region End Fight
    public GameObject endFightPanel;
    public Text endFight_Text;
    public GameObject fightAgainButton;
    public GameObject nextFightButton;
    #endregion

    PlayerDataUnit playerData = PlayerDataTransfer.LoadPlayerData();

    public void EndFightRoutine(Unit winner, Unit loser, int fightCode)
    {
        switch (fightCode)
        {
            //0 - boss won
            //1 - player won
            case 0:
                fightAgainButton.SetActive(true);
                break;

            case 1:
                nextFightButton.SetActive(true);
                break;
        }

        endFightPanel.SetActive(true);
        SetEndFightTexts(winner, loser);
    }

    private void SetEndFightTexts(Unit winner, Unit loser)
    {
        endFight_Text.text = $"{winner.unitName} has defeated {loser.unitName}!";
    }

    public void ReturnToTown_Button()
    {
        //need to set up save/load data here
        PlayerDataTransfer.SavePlayerData(playerData);

        FightManager manager = FindObjectOfType<FightManager>();
        manager.SavePlayerData();

        levelUpIndicator.SetActive(false);

        if (!PlayerDataTransfer.IsOnline)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }

    public void FightAgain_Button()
    {
        //need to set up save/load data here
        PlayerDataTransfer.SavePlayerData(playerData);

        FightManager manager = FindObjectOfType<FightManager>();
        manager.SavePlayerData();

        SceneManager.LoadScene(2);
    }

    public void NextFight_Button()
    {
        //need to set up save/load data here
        PlayerDataTransfer.SavePlayerData(playerData);

        FightManager manager = FindObjectOfType<FightManager>();
        manager.SavePlayerData();

        SceneManager.LoadScene(2);
    }
}
