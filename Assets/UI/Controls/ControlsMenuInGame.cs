using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsMenuInGame : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject GameUI;
    public GameObject p1UI;
    public GameObject p2UI;
    public GameObject controlMenu;
    public GameManager gameManager;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        p1UI.SetActive(false);
        p2UI.SetActive(false);
        Time.timeScale = 1.0f;
        gameManager.paused = false;
        GameUI.SetActive(true);
        controlMenu.SetActive(false);
    }
}
