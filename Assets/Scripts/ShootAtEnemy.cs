using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtEnemy : MonoBehaviour
{
    public LayerMask enemyMask;

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = new Vector3(this.transform.position.x, 1.2f, this.transform.position.z);
        RaycastHit hit;

        Physics.SphereCast(origin, 3f, transform.forward, out hit, )
    }
}