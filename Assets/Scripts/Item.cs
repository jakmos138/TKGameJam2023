using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int value = 70;
    public int type = 0;
    public int level = 1;

    public GameManager gameManager;

    public int Upgrade(int money)
    {
        if (level < 3)
        {
            if (money >= gameManager.prices[type * 3 + level])
            {
                value += Mathf.FloorToInt(gameManager.prices[type * 3 + level] * 0.7f);
                level += 1;
                money -= gameManager.prices[type * 3 + level];
            }
        }
        return money;
    }
}
