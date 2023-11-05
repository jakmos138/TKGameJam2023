using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceOutline : MonoBehaviour
{
    public Outline outline;
    public LayerMask playerMask;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, 1f, playerMask);
        if (objects.Length == 0 || Vector3.Distance(transform.position, player.transform.position) != player.GetComponent<PlayerMovement>().distanceToItem)
        {
            outline.OutlineWidth = 0f;
        }
    }
}
