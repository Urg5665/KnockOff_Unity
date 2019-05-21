using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ReadyAudioObj;
    public GameObject PauseMenu;
    public GameObject GameUI;
    public GameObject p1UI;
    public GameObject p2UI;
    public bool paused;
    public int StartFreeze;
    //public int textTimer;

    public DeathPlane deathPlane;

    public int fixTimeFreezeBug;
    public bool fixTimeEnable;
    public int restartFreeze;

    public GameObject CenterText;

    public Sprite Ready;
    public Sprite Fight;
    public Sprite blueWin;
    public Sprite redWin;

    public GameObject CenterCountdown;

    public Sprite img1;
    public Sprite img2;
    public Sprite img3;

    void Start()
    {
        fixTimeFreezeBug = 0;
        fixTimeEnable = true;
        Instantiate(ReadyAudioObj);
        StartFreeze = 0;
        restartFreeze = 300;
        PauseMenu.SetActive(false);
        paused = false;
        CenterText.GetComponent<Image>().sprite = Ready;
        CenterText.SetActive(true);
        CenterCountdown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartFreeze++;
        if (StartFreeze < 90)
        {
            Time.timeScale = 0.1f;
        }
        if (StartFreeze == 130)
        {
            CenterText.GetComponent<Image>().sprite = Fight;
        }
        if (StartFreeze == 160)
        {
            CenterText.SetActive(false);
        }
        if (deathPlane.player2Score <= 0)
        {
            CenterText.SetActive(true);
            CenterText.GetComponent<Image>().sprite = blueWin;
            restartFreeze--;
            fixTimeEnable = false;
            Time.timeScale = 0.5f;
        }
        if (deathPlane.player1Score <= 0)
        {
            CenterText.SetActive(true);
            CenterText.GetComponent<Image>().sprite = redWin;
            restartFreeze--;
            fixTimeEnable = false;
            Time.timeScale = 0.5f;
        }
        if (restartFreeze < 150)
        {
            CenterText.SetActive(false);
            CenterCountdown.SetActive(true);
            CenterCountdown.GetComponent<Image>().sprite = img3;

            //text.text = "  ... 3 ...";

        }
        if (restartFreeze < 100)
        {
            //text.text = "  ... 2 ...";
            CenterCountdown.GetComponent<Image>().sprite = img2;
        }
        if (restartFreeze < 50)
        {
            //text.text = "  ... 1 ...";
            CenterCountdown.GetComponent<Image>().sprite = img1;
        }
        if (restartFreeze < 0)
        {
            SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 2))); // noninclusive
            //SceneManager.LoadScene(1);
        }



        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 2)));
            //SceneManager.LoadScene(1);
        }
        if (Input.GetKey(KeyCode.Alpha8)) // Press 8 and 9 to speed or slow game, Degbugging
        {
            Time.timeScale += 0.1f;
            //Debug.Log("Speeding Up");
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            Time.timeScale -= 0.1f;
            //Debug.Log("Slowing Down");
        }
        //Debug.Log(Time.timeScale);


        if (Time.timeScale <= .2f && fixTimeEnable && paused == false)
        {
            fixTimeFreezeBug++;
        }
        if (fixTimeFreezeBug > 15 && paused == false)
        {
            Time.timeScale = 1.0f;
            fixTimeFreezeBug = 0;
        }
        if (Input.GetKey(KeyCode.Escape) && paused == false)
        {
            paused = true;
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
            Cursor.visible = true;
            GameUI.SetActive(false);
            p1UI.SetActive(false);
            p2UI.SetActive(false);
        }

    }
       
}
