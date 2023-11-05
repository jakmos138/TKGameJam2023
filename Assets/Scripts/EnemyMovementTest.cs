using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyMovementTest : MonoBehaviour
{
    [Header("Stats")]
    public int hp;
    public float speedMult;
    public int resistance;
    public int reward;
    public int hpCost;

    [Header("References")]
    public Vector3[] cornerPoints;
    private int currentPoint = 1;
    private float speed = 2f;
    int direction = 1;
    float distanceTravelled = 0f;
    GameManager gameManager;
    public Animator animator;
    float slowTime = 0f;
    int slowPower;
    float timeTillDeath = 2.4f;
    private PlayerMovement player;

    private void Start()
    {
        transform.position = new Vector3(cornerPoints[0][0], transform.position.y, cornerPoints[0][2]);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if(hp <= 0)
        {
            animator.SetTrigger("Dead");
            speed = 0;
            GetComponent<Collider>().enabled = false;
            timeTillDeath -= Time.deltaTime;
        }

        if(timeTillDeath < 0)
        {
            Destroy(gameObject);
        }

        float step = speed * speedMult * Time.deltaTime;
        if (slowTime > 0f)
        {
            step *= (1-slowPower/100f);
            slowTime -= Time.deltaTime;
        }
        distanceTravelled += step;
        Vector3 curTarget = new Vector3(cornerPoints[currentPoint].x, transform.position.y, cornerPoints[currentPoint].z);
        transform.position = Vector3.MoveTowards(transform.position, curTarget, step);
        Quaternion toRotation = Quaternion.LookRotation((curTarget - transform.position).normalized, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);

        if (transform.position == curTarget)
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

    public void SlowDown(float slowTime, int damage)
    {
        this.slowTime = slowTime;
        this.slowPower = damage;
    }

    public bool IsSlowed()
    {
        return slowTime > 0f;
    }

    public void TakeDamage(int damage, int type)
    {
        if ((type == 3 && resistance == 2) || (type != 3 && resistance == 1))
        {
            hp -= Mathf.RoundToInt(damage * 0.7f);
        }
        else
        {
            hp -= damage;
        }
    }

    private void OnDestroy()
    {
        gameManager.enemiesToKill--;
        player.money += reward;
    }
}
