using UnityEngine.UI;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("textFields")]
    public Text LivesUi;
    public Text MoneyUi;

    [Header("Stats")]
    public static int money;
    public int startMoney = 1000;
    public static int Lives;
    public int startLives = 10;
    public static int KillCount;
    public static int rounds;
    public static int Score;
    public static int TotalScore;

    void Start()
    {
        money = startMoney;
        Lives = startLives;
        KillCount = 0;
        rounds = 0;
        Score = 0;
    }

    void Update()
    {
        MoneyUi.text = "$" + money;
        LivesUi.text = Lives + " Lives";
    }

    
}
