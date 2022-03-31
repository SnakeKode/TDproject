using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public int waypointIndex = 0;
    public static bool stop = false;
    public float startSpeed = 10f;
    public float speed;
    public bool slowed = false;

    void Start()
    {
        target = waypoints.points[0];
        speed = startSpeed;
    }

    void Update()
    {
        if (stop)
            return;
        
        
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            getNextWaypoint();
        }

        speed = startSpeed;
        
    }

    public void Slow(float cc)
    {
        speed = startSpeed * (1f - cc);
    }

    //a method to search through the waypoints and set the next waypoint as the target
    void getNextWaypoint()
    {
        //call PathEnd method if there are no more waypoints and exit the script
        if (waypointIndex >= waypoints.points.Length - 1)
        {
            PathEnd();
            return;
        }

        waypointIndex++;
        target = waypoints.points[waypointIndex];   //increment the waypointIndex and set the waypoint with the new Index as target
    }

    //a method that decreases lives by 1 and destroys the game object
    void PathEnd()
    {
        Stats.Lives--;
        waveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
