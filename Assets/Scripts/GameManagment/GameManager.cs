using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int fixTimeFreezeBug;

    void Start()
    {
        fixTimeFreezeBug = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
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


        if ( Time.timeScale <= .2f)
        {
            fixTimeFreezeBug++;
        }
        if (fixTimeFreezeBug > 15)
        {
            Time.timeScale = 1.0f;
            fixTimeFreezeBug = 0;
        }


    }
}
