using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameManager gameManager;

    public void Resume()
    {
        gameManager.paused = false;
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        Cursor.visible = false;
    }
}
