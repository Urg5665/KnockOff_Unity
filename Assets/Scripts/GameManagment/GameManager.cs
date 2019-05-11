using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ReadyAudioObj;
    public GameObject PauseMenu;
    public bool paused;
    public int StartFreeze;
    //public int textTimer;

    public DeathPlane deathPlane;

    public int fixTimeFreezeBug;
    public bool fixTimeEnable;
    public Text text;
    public int restartFreeze;

    void Start()
    {
        fixTimeFreezeBug = 0;
        fixTimeEnable = true;
        Instantiate(ReadyAudioObj);
        StartFreeze = 0;
        restartFreeze = 300;
        text = GameObject.Find("Ready...Fight").GetComponent<Text>();
        PauseMenu.SetActive(false);
        paused = false;
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
            text.text = "FIGHT!";
        }
        if (StartFreeze == 160)
        {
            text.text = "";
        }
        if (deathPlane.player2Score <= 0)
        {
            text.text = "BLUE WINS!";
            restartFreeze--;
            fixTimeEnable = false;
            Time.timeScale = 0.5f;
        }
        if (deathPlane.player1Score <= 0)
        {
            text.text = "RED WINS!";
            restartFreeze--;
            fixTimeEnable = false;
            Time.timeScale = 0.5f;
        }
        if (restartFreeze < 150)
        {
            text.text = "  ... 3 ...";

        }
        if (restartFreeze < 100)
        {
            text.text = "  ... 2 ...";
        }
        if (restartFreeze < 50)
        {
            text.text = "  ... 1 ...";
        }
        if (restartFreeze < 0)
        {
            SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 4))); // noninclusive
            //SceneManager.LoadScene(1);
        }



        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(Mathf.RoundToInt(Random.Range(1, 4)));
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
        }

    }
       
}
