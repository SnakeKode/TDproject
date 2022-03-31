using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string lvl = "LevelSelect";
    public Text usernameText;
    public Text Data;
    public sceneFader sceneFader;
    public Leaderboard ld;
    public GameObject leaderboardUI;
    public GameObject mainMenuUI;

    void Start()
    {
        if (AccountManager.IsLoggedIn)
        {
            usernameText.text = "Logged in as : "+AccountManager.LoggedIn_Username;
            AccountManager.instance.LoadData(onRecievedData);
            
        }
    }

    public void play()
    {
        sceneFader.fadeTo(lvl);
    }



    public void leaderBoards()
    {
        mainMenuUI.SetActive(false);
        leaderboardUI.SetActive(true);
    }
   
    public void mainMenu()
    {
        mainMenuUI.SetActive(true);
        leaderboardUI.SetActive(false);
    }

    public void LogOut()
    {
        AccountManager.instance.logOut();
    }

    void onRecievedData(string data)
    {
        PlayerPrefs.SetInt("levelReached", DataTranslator.DataToLevel(data));
        Stats.TotalScore = DataTranslator.DataToScore(data);
        Data.text = "SCORE : " + DataTranslator.DataToScore(data);
        Debug.Log(data);
        ld.addNewHighscore(AccountManager.LoggedIn_Username, Stats.TotalScore);
    }

}
