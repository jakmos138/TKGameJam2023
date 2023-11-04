using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtEnemy : MonoBehaviour
{
    public LayerMask enemyMask;
    public GameObject bullet;
    public float attDelay;
    private float curAttDelay = 0f;

    private void Start()
    {
        curAttDelay = attDelay;
    }

    void Update()
    {
        
        Vector3 origin = new Vector3(transform.position.x, 1.2f, transform.position.z);
        Collider[] hit = Physics.OverlapSphere(origin, 4.5f, enemyMask);

        if (hit.Length > 0)
        {
            float maxDistance = -1f;
            int id = -1;

            for (int i = 0; i < hit.Length; i++)
            {
                float distance = hit[i].transform.GetComponent<EnemyMovementTest>().GetDistanceTravelled();
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    id = i;
                }
            }
            Vector3 target = new Vector3(hit[id].transform.position.x, transform.position.y, hit[id].transform.position.z);
            if (target != new Vector3(0, 0.6f, 0))
            {
                transform.LookAt(target);
            }
            if (curAttDelay <= 0f)
            {
                GameObject attack = Instantiate(bullet, transform.position, Quaternion.identity);
                attack.transform.GetComponent<FollowTarget>().SetParameters(hit[id].transform, 18f);
                curAttDelay = attDelay;
            }
            else
            {
                curAttDelay -= Time.deltaTime;
            }
                
        }
      
    }
}
