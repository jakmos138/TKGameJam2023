using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform target;
    private float speed;
    
    void Update()
    {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);
    }

    public void SetParameters(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }
}
