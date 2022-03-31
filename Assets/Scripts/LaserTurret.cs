using UnityEngine;

public class LaserTurret : MonoBehaviour
{

    private Transform target;
    private Enemy targetEnemy;

    [Header("Setup")]
    public string enemyTag = "enemy";
    public Transform rotation;
    public Transform firePoint;
    public LineRenderer LR;
    public ParticleSystem ImpactEffect;
    public Light ImpactLight;

    [Header("General")]
    public float range = 15f;
    public float rotationSpeed = 2f;

    public int damageOverTime = 5;
    public float CC = .7f;

    

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
        {

            LR.enabled = false;
            ImpactEffect.Stop();
            ImpactLight.enabled = false;
            return;
        }

        //lock on target smoothly
        LockOnTarget();

        Laser();

    }




    void Laser()
    {
        targetEnemy.takeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.GetComponent<EnemyMovement>().Slow(CC);

        if (!LR.enabled)
        {
            LR.enabled = true;
            ImpactEffect.Play();
            ImpactLight.enabled = true;

        }
        LR.SetPosition(0, firePoint.position);
        LR.SetPosition(1, target.position);

        Vector3 dir = firePoint.transform.position - target.position;       //get a direction back to the fire point
        ImpactEffect.transform.rotation = Quaternion.LookRotation(dir);     //look at a direction
        ImpactEffect.transform.position = target.position + dir.normalized;
    }



    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotate = Quaternion.Lerp(rotation.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotation.rotation = Quaternion.Euler(0f, rotate.y, 0f);
    }

   



}
