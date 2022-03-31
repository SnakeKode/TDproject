using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startHealth = 100f;
    public int Worth = 50;
    public int scoreWorth = 100;
    public GameObject DeathEffect;
    public Image healthBar;
    private float health;

    public bool isDead = false;


    void Start()
    {
        health = startHealth;
    }

    void Update()
    {

        if (Manager.GameEnded)
        {
            boom();
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && isDead == false)
        {
            die();
            Stats.KillCount++;
        }
    }

    //a method that destroys the gameObject
    void die()
    {
        isDead = true;
        Destroy(gameObject);

        Stats.money += Worth;
        Stats.Score += scoreWorth;
        waveSpawner.enemiesAlive--;

        GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
    }  
    
    public void boom()
    {
        isDead = true;
        GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, Quaternion.identity);
        waveSpawner.enemiesAlive--;

        Destroy(gameObject);
        Destroy(effect, 2f);
        Stats.Score += scoreWorth;
    }
}
