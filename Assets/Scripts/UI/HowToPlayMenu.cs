using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlayMenu : MonoBehaviour
{
    public int pageNum; // one less than actually page num cause indexing
    public GameObject mainMenu;
    public GameObject controlMenu;
    public GameObject howToMenu;

    public GameObject pageFrame;

    public Sprite[] pages;

    public GameObject left;
    public GameObject right;
    public GameObject play;

    private void Start()
    {
        pageNum = 0;
    }
    public void Update()
    {
        right.SetActive(true);
        left.SetActive(true);
        play.SetActive(false);
        if (pageNum == 0)
        {
            left.SetActive(false);
        }
        if (pageNum == 4)
        {
            right.SetActive(false);
            play.SetActive(true);
        }

        pageFrame.GetComponent<Image>().sprite = pages[pageNum];
    }

    public void Forward()
    {
        pageNum++;
    }
    public void Backward()
    {
        pageNum--;
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        howToMenu.SetActive(false);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 2)));
    }
}
