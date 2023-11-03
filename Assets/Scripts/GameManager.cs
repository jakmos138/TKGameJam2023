using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currentRound = 1;
    private int difficulty = 1;
    [SerializeField] Path[] paths;
    [SerializeField] GameObject[] enemies;
    float delay = 1f;

    // Update is called once per frame
    void Update()
    {
        if(delay <= 0f)
        {
            GameObject enemy = Instantiate(enemies[0], this.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyMovementTest>().cornerPoints = paths[0].cornerPoints;

            delay = 1f;
        } 
        else
        {
            delay -= Time.deltaTime;
        }
    }
}
