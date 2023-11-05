using UnityEngine;

public class Item : MonoBehaviour
{
    public int value = 70;
    public int type;
    public int level = 1;
    public int damage;
    public float attDelay;
    public float rangeModifier;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public int Upgrade(int money)
    {
        if (level < 3)
        {
            if (money >= gameManager.prices[level])
            {
                value += Mathf.FloorToInt(gameManager.prices[level] * 0.7f);
                money -= gameManager.prices[level];
                level += 1;
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
