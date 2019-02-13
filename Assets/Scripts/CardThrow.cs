using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardThrow : MonoBehaviour
{

    public float rotSpeed;
    public float throwSpeed;
    public int playerInt;

    public GameObject player1;
    public Transform cardTarget;

    public bool toRes;
    public bool toPlayer;
    public int i;

    private void Start()
    {
        toRes = true;
        toPlayer = false;
        i = 0;
        player1 = GameObject.Find("Player1");
        cardTarget = GameObject.Find("CardTarget(Clone)").GetComponent<Transform>();


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "resNode")
        {
            toRes = false;
            toPlayer = true;
            Debug.Log("hitRes");
            i = 101;
        }
        if (collision.gameObject.tag == "Target")
        {
            toRes = false;
            toPlayer = true;
            Debug.Log("maxRange");
            i = 101;
        }

        if (collision.gameObject.tag == "Player1" && i > 100)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {

        if (toRes)
        {
            transform.LookAt(cardTarget.transform.position);
            transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
            i++;
        }

        if (toPlayer)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed, Space.World);
            transform.position = Vector3.MoveTowards(transform.position, player1.transform.position, throwSpeed * Time.deltaTime);
        }
        if (i > 100)
        {
            toRes = false;
            toPlayer = true;
        }
    }

}

//transform.position = Vector3.MoveTowards(transform.position, player1Aim.position, throwSpeed * Time.deltaTime);