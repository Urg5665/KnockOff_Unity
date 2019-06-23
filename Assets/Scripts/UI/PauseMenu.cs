using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject howToPlayMenu;
    public GameManager gameManager;
    public GameObject gameUI;
    public GameObject p1UI;
    public GameObject p2UI;

    public void Resume()
    {
        gameManager.paused = false;
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        Cursor.visible = false;
        gameUI.SetActive(true);
        //p1UI.SetActive(true);
        //p2UI.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 2)));
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        // Right now this is just an none eidble controls panel
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void OpenHowToPlay()
    {
        pauseMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
    }
}
