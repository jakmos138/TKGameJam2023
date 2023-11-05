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
    private int damage;
    public GameObject explosion;

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
            if (type == 1)
            {
                collision.collider.transform.GetComponent<EnemyMovementTest>().SlowDown(2f, damage);
            }
            else if (type != 2)
            {
                collision.collider.gameObject.GetComponent<EnemyMovementTest>().TakeDamage(damage, type);
            }
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("EndOfMap"))
        {
            Destroy(gameObject);
        }
    }

    public void SetParameters(Transform target, float speed, int homing, int type, int damage)
    {
        this.target = target;
        Vector3 direction = target.position - transform.position;
        this.direction = direction;
        this.speed = speed;
        this.homing = homing;
        this.type = type;
        this.damage = damage;
    }

    private void OnDestroy()
    {
        if (type == 2)
        {
            Collider[] hit = Physics.OverlapSphere(transform.position, 1f, enemyMask);
            for (int i = 0; i < hit.Length; i++)
            {
                hit[i].gameObject.GetComponent<EnemyMovementTest>().TakeDamage(Mathf.RoundToInt(damage), type);
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
        }
    }
}
