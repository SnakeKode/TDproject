using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class DisplayHighscores : MonoBehaviour
{
    public Text[] highscoreTexts;
    Leaderboard highscoresManager;

    void Start()
    {
        for(int i = 0; i < highscoreTexts.Length ; i++)
        {
            highscoreTexts[i].text = i + 1 + ". fetching...";
        }
        highscoresManager = GetComponent<Leaderboard>();

        StartCoroutine("refrechHighscores");

    }

    public void onHighscoresDownloaded(Leaderboard.Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreTexts.Length; i++)
        {
            highscoreTexts[i].text = i + 1 + ". ";
            if (highscoreList.Length > i)
            {
                highscoreTexts[i].text += highscoreList[i].username + " : " + highscoreList[i].score;
            }
        }
    }

    IEnumerator refrechHighscores()
    {
        while (true)
        {
            highscoresManager.downloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }

}
