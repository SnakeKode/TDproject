using UnityEngine;

public class bullet : MonoBehaviour
{

    private Transform target;
    public GameObject impactEffect;

    [Header("Attributes")]
    public float Speed = 100f;
    public float impactRadius = 0f;
    public int damage = 10;


    public void Seek(Transform _target)
    {
        target = _target;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);


    }

    void HitTarget()
    {
        GameObject effectInst = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInst, 3f);

        if (impactRadius > 0f)
        {

            explode();
        } else
        {
            Damage(target);
        }

        
        Destroy(gameObject);
    }


    void explode()
    {
        Collider[] inRange = Physics.OverlapSphere(transform.position, impactRadius);
        foreach (Collider c in inRange)
        {
            if (c.tag == "enemy")
            {
                Damage(c.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        e.takeDamage(damage);
        
    }

    

}
