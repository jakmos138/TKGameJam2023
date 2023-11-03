using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementTest : MonoBehaviour
{

    public Vector3[] cornerPoints;
    private int currentPoint = 1;
    float speed = 3f;
    int direction = 1;

    private void Start()
    {
        this.transform.position = new Vector3(cornerPoints[0][0], cornerPoints[0][1], cornerPoints[0][2]);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, cornerPoints[currentPoint], step);

        if (this.transform.position == cornerPoints[currentPoint])
        {
            if (currentPoint == cornerPoints.Length - 1 || currentPoint == 0)
            {
                direction *= -1;
            }
            currentPoint += direction;
        }
    }
}
