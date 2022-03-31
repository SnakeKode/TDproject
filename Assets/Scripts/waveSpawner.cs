using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class waveSpawner : MonoBehaviour
{
    public Manager GM;

    public Wave[] waves;

    public Transform spawnPoint;                  //reference to the start point of the map

    public static int enemiesAlive;             //keep track of the number of enemies alive

    public static bool stop = false;

    public int waveNb = 0;                        //wave number
    private bool spawned = true;
    public float TBW = 5f;                        //time between spawning waves
    private float countdown = 2f;                //countdown between enemy spawns
    public Text waveTimer;

    private void Awake()
    {
        enemiesAlive = 0;
    }
    void Update()
    {
        if (stop)
            return;

        if(enemiesAlive > 0)
        {
            return;
        }

        if (waveNb == waves.Length)
        {
            GM.winLevel();
            this.enabled = false;
        }

        if (Manager.GameEnded)
        {
            this.enabled = false;
            return;
        }

        if (spawned)
            countdown -= Time.deltaTime;             //decreasing the countdown using Time
        if (countdown <= 0f)
        {
            Stats.rounds++;
            spawned = false;
            StartCoroutine(spawnWave());         //when the countdown reaches 0 start coroutine spawnWave()
            countdown = TBW;
            return;
        }
        
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveTimer.text = string.Format("{0:00}", countdown);
    }

    IEnumerator spawnWave() 
    {
        Wave wave = waves[waveNb];
        enemiesAlive = wave.count;

        for (int i = 0 ; i<wave.count ; i++)
			{
                spawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);      //wait for 0.5 seconds after spawning each enemy (between each for itiration)
			}
        spawned = true;


        waveNb++;

       
    }

    void spawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);         //spawn an enemy at the spawnPoint
    }
}
