using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public readonly string LOGIN_URL = "https://harmsoft.000webhostapp.com/login.php";
    public readonly string REGISTER_URL = "https://harmsoft.000webhostapp.com/register.php";

    public GameObject errorConnectingPanel;
    public GameObject invalidNamePassPanel;

    public IEnumerator ProcessRequest(string username, string password, string url)
    {
        if(url == LOGIN_URL)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("password", password);

            using (UnityWebRequest request = UnityWebRequest.Post(url, form))
            {
                yield return request.SendWebRequest();

                if (request.isNetworkError)
                {
                    Debug.Log(request.error);
                    errorConnectingPanel.SetActive(true);
                }
                else
                {
                    //Debug.Log(request.downloadHandler.text);

                    //TODO: Load player data from db into PlayerDataTransfer
                    var parser = gameObject.GetComponentInParent<WebParser>();

                    PlayerDataUnit player = parser.ParsePlayerData(request.downloadHandler.text);

                    PlayerDataTransfer.SavePlayerData(player);

                    PlayerDataTransfer.IsOnline = true;

                    //Load OnlineTownScene
                    SceneManager.LoadScene(4);
                }
            }
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("password", password);
            form.AddField("level", 1);
            form.AddField("power", 10);
            form.AddField("speed", 2);
            form.AddField("maxhp", 50);
            form.AddField("intelligence", 10);

            using (UnityWebRequest request = UnityWebRequest.Post(url, form))
            {
                yield return request.SendWebRequest();

                if (request.isNetworkError)
                {
                    Debug.Log(request.error);
                    errorConnectingPanel.SetActive(true);
                }
                else
                {
                    Debug.Log(request.downloadHandler.text);
                }
            }
        }

        #region Deprecated WWW system
        //WWW request = new WWW(url, form);

        //yield return request;

        //if (string.IsNullOrEmpty(request.error))
        //{
        //    Debug.Log(request.text);
        //}
        //else
        //{
        //    Debug.Log(request.text);
        //}
        #endregion

        Debug.Log("response processed");
    }

    public void OnClick_CloseConnectionErrorPanel()
    {
        errorConnectingPanel.SetActive(false);
    }

    public void OnClick_CloseInvalidNamePassPanel()
    {
        invalidNamePassPanel.SetActive(false);
    }
}
