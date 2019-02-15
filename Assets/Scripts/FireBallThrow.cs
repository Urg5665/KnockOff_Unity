using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallThrow : MonoBehaviour
{
    public float throwSpeed;
    public int playerInt;
    public int cardNum;

    public GameObject player1;
    public GameObject player1Aim;

    public int i;

    private void Start()
    {
        i = 0;
        player1 = GameObject.Find("Player1");
        player1Aim = GameObject.Find("Player1Aim");
        transform.LookAt(player1Aim.transform);
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player2")
        {
            collision.gameObject.transform.position = 
                new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 5, collision.gameObject.transform.position.z);
            Destroy(this.gameObject);

        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
        i++;

        if (i > 100)
        {
            Destroy(this.gameObject);
        }
    }
}
