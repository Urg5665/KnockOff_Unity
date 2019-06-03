using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPlane : MonoBehaviour
{
    public int player1Score;
    public int player2Score; // Score is now your health

    public GameObject player1;
    public GameObject player2;
    public GameObject player2Aim;


    public GameObject dummy;

    public Text player1ScoreText;
    public Text player2ScoreText;

    public GameObject[] player1Lives;
    public GameObject[] player2Lives;

    public CameraMove cameraMove;

    public AudioSource audioSource;

    public bool uiAnimP1;
    public bool uiAnimP2;
    public int uiP1Time;
    public int uiP2Time;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        player1Score = 3;
        player2Score = 3;
        uiAnimP1 = false;
        uiAnimP2 = false;
        uiP1Time = 0;
        uiP2Time = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            player1Score--;
            player1.transform.position = new Vector3(-25,10f,-37);
            player2.transform.position = new Vector3(44, 10f, -36);
            player2Aim.transform.position = new Vector3(40, 10f, -33);
            player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(cameraMove.Shake(.3f, 1f));
            audioSource.Play();
            uiAnimP1 = true;
            //print("Collider Kill");

        }
        if (collision.gameObject.tag == "Player2")
        {
            player2Score--;
            player1.transform.position = new Vector3(-25, 10f, -37);
            player2.transform.position = new Vector3(44, 10f, -36);
            player2Aim.transform.position = new Vector3(40, 10f, -33);
            player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(cameraMove.Shake(.3f, 1f));
            audioSource.Play();
            //Destroy(player2Lives[player2Score]);
            uiAnimP2 = true;
            //print("Collider Kill");
        }
        if (collision.gameObject.tag == "Dummy")
        {
            dummy.transform.position = new Vector3(25, 2.5f, 2);
            dummy.GetComponent<Rigidbody>().velocity = Vector3.zero;
            dummy.GetComponent<DummyMovement>().i = 0;
            StartCoroutine(cameraMove.Shake(.3f, 1f));
            audioSource.Play();
        }

    }

    private void Update()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
        if (uiAnimP1)
        {
            uiP1Time++;
            if (uiP1Time < 100)
            {
                if (uiP1Time <= 10)
                {
                    player1Lives[player1Score].transform.eulerAngles += new Vector3(0.00f, 0.00f, 4.5f);
                }
                player1Lives[player1Score].transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }

            if (uiP1Time > 100)
            {
                uiAnimP1 = false;
                Destroy(player1Lives[player1Score].gameObject);
                uiP1Time = 0; 
            }
        }
        if (uiAnimP2)
        {
            uiP2Time++;
            if(uiP2Time < 100)
            {
                if (uiP2Time <= 10)
                {
                    player2Lives[player2Score].transform.eulerAngles += new Vector3(0.00f, 0.00f, 4.5f);
                }
                player2Lives[player2Score].transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }

            if (uiP2Time > 100)
            {
                uiAnimP2 = false;
                Destroy(player2Lives[player2Score]);
                uiP2Time = 0;
                
            }
        }


        if ( player1.transform.position.y < -17)
        {
            player1Score--;
            player1.transform.position = new Vector3(-25, 10f, -37);
            player2.transform.position = new Vector3(44, 10f, -36);
            player2Aim.transform.position = new Vector3(40, 10f, -33);
            player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(cameraMove.Shake(.3f, 1f));
            audioSource.Play();
            //Destroy(player1Lives[player1Score]);
            uiAnimP1 = true;
            //print("Height Kill");
        }
        if (player2.transform.position.y < -17)
        {
            player2Score--;
            player1.transform.position = new Vector3(-25, 10f, -37);
            player2.transform.position = new Vector3(44, 10f, -36);
            player2Aim.transform.position = new Vector3(40, 10f, -33);
            player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(cameraMove.Shake(.3f, 1f));
            audioSource.Play();
            //Destroy(player2Lives[player2Score]);
            uiAnimP2 = true;
            //print("Height Kill");
        }
    }
}
