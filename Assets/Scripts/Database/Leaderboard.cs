using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    const string privateCode = "nOpXgMLTskGgEeASgaeyEAXYEi4hmCVEK1LVKNhyaShA";
    const string publicCode = "5ef45bba377eda0b6c6eae89";
    const string WebURL = "http://dreamlo.com/lb/";


    public Highscore[] highscoresList;
    public DisplayHighscores displayHighscores;


    private void Awake()
    {
        
        downloadHighscores();
        displayHighscores = GetComponent<DisplayHighscores>();
    }

    public void addNewHighscore(string username, int score)
    {
        StartCoroutine(uploadScore(username, score));
    }

    IEnumerator uploadScore(string username, int score)
    {
        UnityWebRequest www = UnityWebRequest.Get(WebURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        yield return www.SendWebRequest();
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("upload sccess!");
        }
        else
        {
            Debug.Log("upload Failed : " + www.error);
        }
    }




    public void downloadHighscores()
    {
        StartCoroutine("downloadScores");
    }

    IEnumerator downloadScores()
    {
        UnityWebRequest www = UnityWebRequest.Get(WebURL + publicCode + "/pipe/");
        yield return www.SendWebRequest();
        if (string.IsNullOrEmpty(www.error))
        {
            formatHighscore(www.downloadHandler.text);
            displayHighscores.onHighscoresDownloaded(highscoresList);
        }
        else
        {
            Debug.Log("upload Failed : " + www.error);
        }
    }


    void formatHighscore(string data)
    {
        string[] parts = data.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[parts.Length];
        for (int i = 0 ; i < parts.Length ; i++)
        {
            string[] info = parts[i].Split('|');
            string username = info[0];
            int score = int.Parse(info[1]);
            highscoresList[i] = new Highscore(username, score);
            Debug.Log(highscoresList[i].username + " : " + highscoresList[i].score);
        }
    }

    [System.Serializable]
    public struct Highscore
    {
        public string username;
        public int score;

        public Highscore(string _username, int _score)
        {
            username = _username;
            score = _score;
        }
    }

}
