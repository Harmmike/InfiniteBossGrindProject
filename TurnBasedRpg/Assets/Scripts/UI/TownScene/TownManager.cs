using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TownManager : MonoBehaviour
{
    public GameObject characterPanel;
    public GameObject availableSkillsPanel;
    public GameObject newSkillsPanel;
    public GameObject saveSuccessPanel;

    public List<GameObject> allPanels = new List<GameObject>();

    private void Start()
    {
        allPanels.Add(characterPanel);
        allPanels.Add(availableSkillsPanel);
        allPanels.Add(newSkillsPanel);
        allPanels.Add(saveSuccessPanel);
    }

    public void NewFight_Button()
    {
        SceneManager.LoadScene(2);
    }

    public void Character_Button()
    {
        if (CheckForOpenPanels())
        {
            return;
        }

        characterPanel.GetComponent<CharacterPanel>().UpdateTextFields();

        characterPanel.SetActive(true);
    }

    public void MySkills_Button()
    {
        if (CheckForOpenPanels())
        {
            return;
        }

        availableSkillsPanel.GetComponent<AvailableSkills_UI>().UpdateUI();

        availableSkillsPanel.SetActive(true);
    }

    public void NewSkills_Panel()
    {
        if (CheckForOpenPanels())
        {
            return;
        }

        newSkillsPanel.GetComponent<NewSkillsPanel>().UpdateUI();

        newSkillsPanel.SetActive(true);
    }

    private bool CheckForOpenPanels()
    {
        foreach (var panel in allPanels)
        {
            if (panel.activeSelf)
            {
                return true;
            }
        }

        return false;
        //here we will flag a popup for open panels.
    }

    #region IO
    public void Save_Button()
    {
        try
        {
            SaveScript.SaveData(PlayerDataTransfer.LoadPlayerData());
            StartCoroutine(SaveSuccessRoutine());
        }
        catch (System.Exception)
        {
            Debug.Log("save error");
        }
    }

    private IEnumerator SaveSuccessRoutine()
    {
        saveSuccessPanel.SetActive(true);

        yield return new WaitForSeconds(2);

        saveSuccessPanel.SetActive(false);
    }
    #endregion
}
