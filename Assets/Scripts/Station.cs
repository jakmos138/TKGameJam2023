using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public int type = 0;
    public int cost = 100;
    public GameObject[] spawnPoints;
    [SerializeField] GameManager gameManager;
    private bool firstSummon = true;
    System.Random nonSeededRandom;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        nonSeededRandom = new System.Random();
    }

    public int Interact(int money)
    {
        if (type == 0)
        {
            int id = -1;
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].transform.childCount == 0)
                {
                    id = i;
                    break;
                }
            }
            if (money >= cost && id != -1)
            {
                money -= cost;
                if (firstSummon)
                {
                    int index = nonSeededRandom.Next(0, 4);
                    if (index == 1)
                    {
                        index = 0;
                    }
                    Instantiate(gameManager.towers[index * 2], spawnPoints[id].transform);
                    firstSummon = false;
                } else
                {
                    Instantiate(gameManager.towers[nonSeededRandom.Next(0, 4) * 2], spawnPoints[id].transform);
                }
            }
        }
        return money;
    }
}
