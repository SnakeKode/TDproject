using UnityEngine;
using UnityEngine.UI;
public class PowerMoves : MonoBehaviour
{
    public GameObject pauseUI;
    public Text DCost;
    public Text SCost;
    public int destroyCost = 500;
    public int stopCost = 700;
    public float stopCount = 3f;
    float count = 0f;

    void Start()
    {
        
        DCost.text = "$" + destroyCost;
        SCost.text = "$" + stopCost;
    }

    void Update()
    {
        count -= Time.deltaTime;             //decreasing the countdown using Time
        count = Mathf.Clamp(count, 0f, Mathf.Infinity);
        if ((count == 0) && (waveSpawner.stop))
        {
            waveSpawner.stop = false;
            EnemyMovement.stop = false;
        }
    }


    public void DestroyFirst()
    {
        GameObject e = GameObject.FindGameObjectWithTag("enemy");
        if ((e == null)||(Stats.money < destroyCost))
            return;
        Enemy en = (Enemy) e.GetComponent(typeof(Enemy));
        if (!en.isDead)
        {
            en.boom();
            Stats.money -= destroyCost;
        }
        
    }

    public void StopAll()
    {
        count = stopCount;
        if ((Stats.money < stopCost)||(waveSpawner.enemiesAlive == 0))
            return;

        Stats.money -= stopCost;
        EnemyMovement.stop = true;
        waveSpawner.stop = true;

    }

    public void pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

}
