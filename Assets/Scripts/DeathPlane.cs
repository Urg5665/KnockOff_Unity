using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public int player1Score;
    public int player2Score;

    public GameObject player1;
    public GameObject player2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            player2Score++;
            player1.transform.position = new Vector3(-10,2.5f,2);
            player1.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }
        if (collision.gameObject.tag == "Player2")
        {
            player1Score++;
            player2.transform.position = new Vector3(30, 2.5f, 2);
            player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
