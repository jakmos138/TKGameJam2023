using UnityEngine;

public class Item : MonoBehaviour
{
    public int value = 70;
    public int type;
    public int level = 1;
    public int damage;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

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

        Material material = transform.parent.GetComponent<Renderer>().material;
        if (level == 2)
        material.color = Color.white;
        else if (level == 3)
        material.color = Color.black;

        return money;
    }
}
