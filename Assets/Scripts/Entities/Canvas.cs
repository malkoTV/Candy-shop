﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject scoreText;
    private int totalCoins = 0;

    void Start()
    {
        GlobalVariables.CanvasObj = gameObject;

        EventManager.AddListener(EventName.OrderCompletedEvent, CoinsAdded);
        EventManager.AddListener(EventName.GameLostEvent, GameLost);

        scoreText = gameObject.transform.GetChild(0).gameObject;

        scoreText.GetComponent<Text>().text = totalCoins.ToString();
        pauseMenu = gameObject.transform.GetChild(1).gameObject;
    }

    //make unity int event
    void CoinsAdded(int coins)
    {
        totalCoins += coins;
        scoreText.GetComponent<Text>().text = totalCoins.ToString();
        if(totalCoins >= GlobalVariables.WinScore)
        {
            //invoke events for all spawners
            //invoke event for player to stop shooting
            //invoke event for all
            GameOver();
        }
    }

    void GameLost(int i) //useless parameter
    {
        GameOver();
        //set test to game lost
    }

    void GameOver()
    {
        Time.timeScale = 0f;

        pauseMenu.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayScene");
    }
}
