using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject selectLevel;
    public GameObject whoPlay;

    public void OnePlayer()
    {
        selectLevel.SetActive(true);
        whoPlay.SetActive(false);
        GameMode.twoPlayer = false;
    }

    public void TwoPlayer()
    {
        GameMode.twoPlayer = true;
        SceneManager.LoadScene(1);
    }

    public void LevelEasy()
    {
        GameMode.levelAI = 1;
        SceneManager.LoadScene(1);
    }

    public void LevelMedium()
    {
        GameMode.levelAI = 2;
        SceneManager.LoadScene(1);
    }
    public void LevelHard()
    {
        GameMode.levelAI = 3;
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        selectLevel.SetActive(false);
        whoPlay.SetActive(true);
    }
}
