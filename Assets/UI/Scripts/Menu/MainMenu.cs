using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int currentImg = 0;
    public GameObject[] imgs;
    public GameObject menu;
    public GameObject tutorial;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void PlayTutorial()
    {
        currentImg = 0;
        menu.SetActive(false);
        tutorial.SetActive(true);
        ImgActivate();
    }

    public void NextImage()
    {
        currentImg++;
        if (currentImg >= imgs.Length)
        {
            ReturnToMenu();
        } else
        {
            ImgActivate();
        }
    }

    public void ImgActivate()
    {
        for (int i = 0; i < imgs.Length; i++)
        {
            imgs[i].SetActive(i==currentImg);
        }
    }

    public void ReturnToMenu()
    {
        menu.SetActive(true);
        tutorial.SetActive(false);
    }

}
