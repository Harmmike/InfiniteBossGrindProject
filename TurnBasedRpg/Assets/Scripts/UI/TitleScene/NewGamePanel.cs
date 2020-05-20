using UnityEngine;
using UnityEngine.UI;

public class NewGamePanel : MonoBehaviour
{
    public InputField heroNameInputField;

    public void Begin_Button()
    {
        if(string.IsNullOrEmpty(heroNameInputField.text))
        {
            return;
        }
        else
        {
            TitleSceneManager titleSceneMgr = FindObjectOfType<TitleSceneManager>();
            titleSceneMgr.BeginNewGame(heroNameInputField.text);
        }
    }

    public void Back_Button()
    {
        TitleSceneManager titleSceneMgr = FindObjectOfType<TitleSceneManager>();
        titleSceneMgr.GoToTitleScreen();
    }
}
