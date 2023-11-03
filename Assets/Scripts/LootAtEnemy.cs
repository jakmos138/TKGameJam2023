using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAtEnemy : MonoBehaviour
{
    public Transform enemy;

    void Update()
    {
        Vector3 target = new Vector3(enemy.position.x, this.transform.position.y, enemy.position.z);

        this.transform.LookAt(target);
    }
}
