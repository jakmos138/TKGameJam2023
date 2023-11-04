using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives = 20;
    public int enemiesToKill = 200;

    private bool paused = false;
    
    private int currentRound = 1;
    public Path[] paths;
    public GameObject[] enemies;
    public int[] prices;
    public GameObject[] towers;
    float delay = 25f;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject victory;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject ui;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if(delay <= 0f)
        {
            GameObject enemy = Instantiate(enemies[Random.Range(0,4)]);
            enemy.GetComponent<EnemyMovementTest>().cornerPoints = paths[0].cornerPoints;

            delay = 1.5f;
        } 
        else
        {
            delay -= Time.deltaTime;
        }

        if (lives <= 0)
        {
            GameOver();
        }

        if (enemiesToKill <= 0)
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

    private void Pause()
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
}
