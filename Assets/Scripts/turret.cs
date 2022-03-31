using UnityEngine;

public class turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Setup")]
    public string enemyTag = "enemy";
    public Transform rotation;
    public GameObject bulletPre;
    public Transform firePoint;

    [Header("General")]
    public float range = 15f;
    public float rotationSpeed = 2f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;






    void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
    }


    void updateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }

        if ((nearestEnemy != null) && (shortestDistance <= range))
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }


    }


    void Update()
    {
        if (target == null)
            return;

        //lock on target smoothly
        LockOnTarget();



        if (fireCountdown <= 0f)
        {
            Fire();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;                    //fire countdown reduced by 1 each second;


    }

    void Fire()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPre, firePoint.position, firePoint.rotation);       //making a reference to get in the bullet script
        bullet bullet = bulletGO.GetComponent<bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);                            //passing the turret's target to the bullet as a parameter
        }

    }


   

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotate = Quaternion.Lerp(rotation.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotation.rotation = Quaternion.Euler(0f, rotate.y, 0f);
    }

  
}
