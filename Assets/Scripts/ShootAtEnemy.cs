using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtEnemy : MonoBehaviour
{
    public LayerMask enemyMask;
    public GameObject bullet;
    public float attDelay;
    private float curAttDelay = 0f;

    void Update()
    {
        if (curAttDelay <= 0f)
        {
            Vector3 origin = new Vector3(this.transform.position.x, 1.2f, this.transform.position.z);
            RaycastHit[] hit = Physics.SphereCastAll(origin, 3f, transform.forward, 3f, enemyMask);

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
                Vector3 target = new Vector3(hit[id].transform.position.x, this.transform.position.y, hit[id].transform.position.z);
                transform.LookAt(target);
                GameObject attack = Instantiate(bullet, this.transform.position, Quaternion.identity);
                attack.transform.GetComponent<FollowTarget>().SetParameters(hit[id].transform, 5f);
                curAttDelay = attDelay;
            }
        } else
        {
            curAttDelay -= Time.deltaTime;
        }
    }
}
