using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public LayerMask enemyMask;
    private Transform target;
    Vector3 direction;
    private float speed;
    private int homing;
    private int type;

    void Update()
    {
        float step = speed * Time.deltaTime;
        if (homing == 1)
        {
            if (target == null)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, step);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy")) {
            if (type != 1)
            {
                Destroy(collision.collider.gameObject);
            } else
            {
                collision.collider.transform.GetComponent<EnemyMovementTest>().SlowDown(2f);
            }
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("EndOfMap"))
        {
            Destroy(gameObject);
        }
    }

    public void SetParameters(Transform target, float speed, int homing = 0, int type = 0)
    {
        this.target = target;
        Vector3 direction = target.position - transform.position;
        this.direction = direction;
        this.speed = speed;
        this.homing = homing;
        this.type = type;
    }

    private void OnDestroy()
    {
        if (type == 2)
        {
            Collider[] hit = Physics.OverlapSphere(transform.position, 1f, enemyMask);
            for (int i = 0; i < hit.Length; i++)
            {
                Destroy(hit[i].gameObject);
            }
        }
    }
}
