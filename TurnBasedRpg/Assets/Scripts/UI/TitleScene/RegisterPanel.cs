using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour
{
    public InputField usernameInputField;
    public InputField passwordInputField;
    public InputField passwordVerifyInputField;

    public LoginScript loginScript;

    public void Register_Button()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string verify = passwordVerifyInputField.text;

        if (VerifyMatchingPasswords(password, verify))
        {
            StartCoroutine(loginScript.ProcessRequest(username, password, loginScript.REGISTER_URL));
        }
        else
        {
            //set a warning
            return;
        }
    }

    private bool VerifyMatchingPasswords(string passOne, string passTwo)
    {
        bool passwordsMatch = false;

        if(passOne == passTwo)
        {
            passwordsMatch = true;
        }
        else
        {
            passwordsMatch = false;
        }
        return passwordsMatch;
    }
}
