using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderboardPanel : MonoBehaviour
{
    private readonly string LEADERBOARD_URL = "https://harmsoft.000webhostapp.com/IBGLeaderboard.php";

    private Dictionary<string, int> players = new Dictionary<string, int>();

    public GameObject playerContainerPrefab;
    public GameObject scrollableArea;

    public GameObject loadingIcon;
    public Image loadingIconImage;

    private void Update()
    {
        //float imageFill = 0;

        //loadingIconImage.fillAmount = imageFill;

        //imageFill += Time.deltaTime;
    }

    public void UpdateBoard()
    {
        ClearPreviousList();

        loadingIcon.SetActive(true);

        players.Clear();
        


        StartCoroutine(GetPlayersFromDB());
    }

    private IEnumerator GetPlayersFromDB()
    {
        Debug.Log("Getting leaderboard");
        using (UnityWebRequest request = UnityWebRequest.Post(LEADERBOARD_URL, ""))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                

                WebParser parser = GetComponent<WebParser>();

                players = parser.ParseLeaderboardData(request.downloadHandler.text);

                WriteToLeaderboard();
            }
        }
    }

    private void ClearPreviousList()
    {
        var playerObjects = scrollableArea.GetComponentsInChildren<PlayerContainerObject>();

        foreach (var player in playerObjects)
        {
            Destroy(player.gameObject);
        }
    }

    private void WriteToLeaderboard()
    {
        loadingIcon.SetActive(false);

        Debug.Log("writing to leaderboard");
        int rank = 0;
        foreach (var item in players.OrderByDescending(i => i.Value))
        {
            rank++;
            var playerObject = Instantiate(playerContainerPrefab, scrollableArea.transform).GetComponent<PlayerContainerObject>();
            playerObject.rankText.text = rank.ToString();
            playerObject.nameText.text = item.Key;
            playerObject.levelText.text = item.Value.ToString();
        }
    }

    public void ClosePanel()
    {

        this.gameObject.SetActive(false);

        TownManager townManager = FindObjectOfType<TownManager>();
        townManager.megaBossButton.gameObject.SetActive(true);
        townManager.normalBossButton.gameObject.SetActive(true);
    }
}
