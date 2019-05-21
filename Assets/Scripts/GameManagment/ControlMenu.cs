using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject controls;
    public GameObject howToPlay;

    public void ReturnMain()
    {
        main.SetActive(true);
        controls.SetActive(false);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 1)));
    }
    public void LoadInstructions()
    {
        howToPlay.SetActive(true);
        controls.SetActive(false);
    }
}
