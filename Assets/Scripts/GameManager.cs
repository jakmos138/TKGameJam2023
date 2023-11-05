using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 20;
    public int enemiesKilled = 0;

    private int[][] enemyCounts =
    {
        //waves1-5
        new int[] { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[] { 2, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[] { 0, 3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[] { 4, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[] { 10, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
        //waves6-10
        new int[] { 5, 2, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0 },
        new int[] { 0, 15, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
        new int[] { 0, 0, 7, 3, 0, 0, 0, 1, 0, 0, 0, 0 },
        new int[] { 8, 0, 10, 8, 2, 1, 0, 0, 1, 0, 0, 0 },
        new int[] { 0, 30, 5, 0, 0, 2, 2, 0, 1, 0, 0, 0 },
        //waves11-15
        new int[] { 20, 10, 8, 4, 1, 1, 1, 1, 0, 1, 0, 0 },
        new int[] { 12, 0, 8, 6, 0, 2, 1, 1, 1, 1, 0, 0 },
        new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 0, 0, 1, 0 },
        new int[] { 9, 3, 10, 10, 4, 4, 4, 4, 1, 1, 1, 0 },
        new int[] { 0, 10, 10, 10, 0, 5, 5, 5, 0, 0, 0, 1 },
        //waves16-20
        new int[] { 23, 11, 8, 4, 1, 1, 1, 1, 0, 1, 0, 0 },
        new int[] { 0, 0, 0, 0, 10, 5, 10, 10, 1, 1, 2, 2 },
        new int[] { 23, 12, 6, 6, 3, 6, 6, 6, 4, 0, 0, 0 },
        new int[] { 32, 15, 10, 10, 10, 0, 0, 0, 0, 1, 2, 2 },
        new int[] { 48, 20, 30, 20, 3, 4, 7, 7, 2, 2, 2, 2 }
    };

    private int[] spawnCounters = {3,6,4,10,11,12,16,11,30,40,47,32,41,51,46,51,41,72,82,147};
    private int[] totalCounters = {3,6,4,10,11,12,16,11,30,40,47,32,41,51,46,51,41,72,82,147};

    private bool paused = false;
    
    public int currentRound = 1;
    public Path[] paths;
    public GameObject[] enemies;
    public int[] prices;
    public GameObject[] towers;
    float delay = 15f;
    private int chosenPath = 0;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject victory;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject ui;

    public TMPro.TextMeshProUGUI roundText;
    public TMPro.TextMeshProUGUI livesText;

    private void Start()
    {
        Random.InitState(138);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        SetUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (currentRound < 21)
        {
            if (spawnCounters[currentRound - 1] == 0)
            {
                int total = 0;
                for (int i = 0; i < currentRound; i++)
                {
                    total += totalCounters[i];
                }
                if (enemiesKilled == total)
                {
                    currentRound++;
                }
            }

            if (delay <= 0f)
            {
                if (spawnCounters[currentRound - 1] > 0)
                {
                    int value = Mathf.FloorToInt(Random.value * 12);

                    while (enemyCounts[currentRound - 1][value] == 0)
                    {
                        value--;
                        if (value < 0)
                        {
                            value = 11;
                        }
                    }

                    GameObject enemy = Instantiate(enemies[value]);
                    enemyCounts[currentRound - 1][value]--;
                    spawnCounters[currentRound - 1]--;

                    enemy.GetComponent<EnemyMovementTest>().cornerPoints = paths[chosenPath].cornerPoints;
                    chosenPath = (chosenPath + 1) % paths.Length;

                    delay = 1.5f - (0.2f * (currentRound % 5));
                }
            }
            else
            {
                delay -= Time.deltaTime;
            }
        }

        if (lives <= 0)
        {
            GameOver();
        }

        if (currentRound > 20)
        {
            Victory();
        }
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        ui.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    void Victory()
    {
        Time.timeScale = 0;
        victory.SetActive(true);
        ui.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Pause()
    {
        if (paused)
        {
            pauseMenu.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        } else
        {
            pauseMenu.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    void SetUI()
    {
        livesText.text = "Lives: " + lives;
        roundText.text = "Round: " + currentRound + "/20";
    }
}
