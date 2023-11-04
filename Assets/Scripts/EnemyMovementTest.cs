using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementTest : MonoBehaviour
{

    public int hp = 30;
    public Vector3[] cornerPoints;
    private int currentPoint = 1;
    public float speed = 2f;
    int direction = 1;
    float distanceTravelled = 0f;
    int hpCost = 1;
    GameManager gameManager;
    float slowTime = 0f;

    private void Start()
    {
        transform.position = new Vector3(cornerPoints[0][0], cornerPoints[0][1], cornerPoints[0][2]);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }

        float step = speed * Time.deltaTime;
        if (slowTime > 0f)
        {
            step *= 0.5f;
            slowTime -= Time.deltaTime;
        }
        distanceTravelled += step;
        transform.position = Vector3.MoveTowards(transform.position, cornerPoints[currentPoint], step);
        Quaternion toRotation = Quaternion.LookRotation((cornerPoints[currentPoint] - transform.position).normalized, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);

        if (transform.position == cornerPoints[currentPoint])
        {
            if (currentPoint == cornerPoints.Length - 1)
            {
                Destroy(gameObject);
                gameManager.TakeDamage(hpCost);
            }
            currentPoint += direction;
        }
    } 

    public float GetDistanceTravelled()
    {
        return distanceTravelled;
    }

    public void SlowDown(float slowTime)
    {
        this.slowTime = slowTime;
    }

    public bool IsSlowed()
    {
        return slowTime > 0f;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }

    private void OnDestroy()
    {
        gameManager.enemiesToKill--;
    }
}
