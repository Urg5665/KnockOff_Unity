﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void LoadLevel()
    {
        SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 4)));
    }

    public void OpenSettings()
    {

    }

    public void LoadTutorial()
    {

    }
}