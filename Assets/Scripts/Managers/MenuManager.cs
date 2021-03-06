﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    bool paused = false;
    [SerializeField]
    private GameObject pauseText;

   void Start()
   {
        AudioManager.PlayLoop(LoopAudioName.MenuSoundtrack);
   }

   public void PlayGame()
   {
        AudioManager.Play(AudioClipName.ButtonClick);
        SceneManager.LoadScene("PlayScene");
   }

   public void Pause()
    {
        if(!paused)
        {
            AudioManager.Play(AudioClipName.ButtonClick);
            Time.timeScale = 0f;
            paused = true;
            pauseText.SetActive(true);
        }
        else
        {
            AudioManager.Play(AudioClipName.ButtonClick);
            Time.timeScale = 1f;
            paused = false;
            pauseText.SetActive(false);
        }
    }
}
