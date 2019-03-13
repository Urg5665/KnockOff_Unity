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

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        player1Score = 3;
        player2Score = 3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            player1Score--;
            player1.transform.position = new Vector3(-25,5f,-37);
            player2.transform.position = new Vector3(44, 5f, -36);
            player2Aim.transform.position = new Vector3(40, 5f, -33);
            player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(cameraMove.Shake(.3f, 1f));
            audioSource.Play();
            Destroy(player1Lives[player1Score]);

        }
        if (collision.gameObject.tag == "Player2")
        {
            player2Score--;
            player1.transform.position = new Vector3(-25, 5f, -37);
            player2.transform.position = new Vector3(44, 5f, -36);
            player2Aim.transform.position = new Vector3(40, 5f, -33);
            player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(cameraMove.Shake(.3f, 1f));
            audioSource.Play();
            Destroy(player2Lives[player2Score]);
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
    }
}
