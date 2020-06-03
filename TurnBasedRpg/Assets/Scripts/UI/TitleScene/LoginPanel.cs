using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public InputField usernameInputField;
    public InputField passwordInputField;

    public LoginScript loginScript;

    public void Login_Button()
    {
        string user = usernameInputField.text;
        string pass = passwordInputField.text;

        StartCoroutine(loginScript.ProcessRequest(user, pass, loginScript.LOGIN_URL));
    }
}
