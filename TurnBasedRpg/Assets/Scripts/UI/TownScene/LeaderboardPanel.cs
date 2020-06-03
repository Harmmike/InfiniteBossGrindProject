using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderboardPanel : MonoBehaviour
{
    private readonly string LEADERBOARD_URL = "https://harmsoft.000webhostapp.com/IBGLeaderboard.php";

    private Dictionary<string, int> players = new Dictionary<string, int>();

    public GameObject playerContainerPrefab;
    public GameObject scrollableArea;

    public void UpdateBoard()
    {
        Debug.Log("updating leaderboards");

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

    private void WriteToLeaderboard()
    {
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
