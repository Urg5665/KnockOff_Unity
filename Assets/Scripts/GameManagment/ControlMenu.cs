using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject controls;

    public void ReturnMain()
    {
        main.SetActive(true);
        controls.SetActive(false);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 4)));
    }
}
