using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject controls;
    public GameObject howTo;
    

    public void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = true;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 4)));
    }

    public void OpenSettings()
    {
        // Right now this is just an none eidble controls panel
        main.SetActive(false);
        controls.SetActive(true);

    }

    public void LoadTutorial()
    {
        // Right Now this is just an INstructions set of panels
        main.SetActive(false);
        howTo.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();  
    }
}
