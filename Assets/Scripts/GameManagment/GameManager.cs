using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKey(KeyCode.Alpha8)) // Press 1 and 2 to speed or slow game, Degbugging
        {
            Time.timeScale += 0.1f;
            Debug.Log("Speeding Up");
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            Time.timeScale -= 0.1f;
            Debug.Log("Slowing Down");
        }

    }
}
