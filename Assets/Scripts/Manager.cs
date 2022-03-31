using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject LevelWonUI;

    public static bool GameEnded;

    public sceneFader sceneFader;

    public int unlock = 2;

    public int LvlTag = 1;

    int level;


    void Start()
    {
        level = PlayerPrefs.GetInt("levelReached", 1);
        GameEnded = false;
    }
    void Update()
    {
        if (GameEnded)
            return;

        if (Stats.Lives <= 0)
        {
            EndGame();
        }

        if (Input.GetKey("e"))
        {
            EndGame();
        }


        if (Input.GetKey("w"))
        {
            winLevel();
        }
    }

    void EndGame()
    {
        GameEnded = true;
        GameOverUI.SetActive(true);
        updateData();
        Debug.Log("Killed units = " + Stats.KillCount);
        Debug.Log("SCORE : " + Stats.Score);
    }


    public void winLevel()
    {
        Debug.Log("LEVEL WON");
        if(level < unlock)
        {
            PlayerPrefs.SetInt("levelReached", unlock);
            level = unlock;
        }
        updateData();

        LevelWonUI.SetActive(true);
        GameEnded = true;
    }


    public void updateData()
    {
        if (AccountManager.IsLoggedIn)
        {
            AccountManager.instance.LoadData(onDataReceived);
        }
    }


    public void onDataReceived(string data)
    {
        int score1 = DataTranslator.DataToLv1Score(data);
        int score2 = DataTranslator.DataToLv2Score(data);
        int score3 = DataTranslator.DataToLv3Score(data);

        string newData = data;

        switch (LvlTag)
        {
            case 1:if(Stats.Score > score1)
                {
                    newData = DataTranslator.ValuesToData(Stats.Score, score2, score3, level);
                }break;

            case 2:
                if (Stats.Score > score2)
                {
                    newData = DataTranslator.ValuesToData(score1, Stats.Score, score3, level);
                }
                break;

            case 3:
                if (Stats.Score > score1)
                {
                    newData = DataTranslator.ValuesToData(score1, score2, Stats.Score, level);
                }
                break;
        }
            

        AccountManager.instance.SaveData(newData);
        
    }
}
