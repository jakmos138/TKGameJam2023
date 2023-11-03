using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform target;
    Vector3 direction;
    private float speed;
    private int homing;

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
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, step);
            }
        } else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + direction, step);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy")) {
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        } 
        else if (collision.collider.CompareTag("EndOfMap"))
        {
            Destroy(gameObject);
        }
    }

    public void SetParameters(Transform target, float speed, int homing = 0)
    {
        this.target = target;
        Vector3 direction = target.position - this.transform.position;
        this.direction = direction;
        this.speed = speed;
        this.homing = homing;
    }
}
