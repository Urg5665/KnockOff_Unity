using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameManager gameManager;
    public GameObject gameUI;

    public void Resume()
    {
        gameManager.paused = false;
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        Cursor.visible = false;
        gameUI.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 4)));
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
