using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindWaveThrow : MonoBehaviour
{
    public float throwSpeed;
    public int playerInt;
    public int spellNum;

    public GameObject player1;
    public GameObject player1Aim;
    public PlayerControl playerControl;

    public int i;

    private void Start()
    {
        i = 0;
        player1 = GameObject.Find("Player1");
        player1Aim = GameObject.Find("Player1Aim");
        transform.LookAt(player1Aim.transform);
        playerControl = player1.GetComponent<PlayerControl>();
        spellNum = playerControl.spellSelected;
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player2")
        {
            collision.gameObject.transform.position =
                new Vector3(50, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
            Destroy(this.gameObject);
            playerControl.canCast[spellNum] = true;

        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
        i++;

        if (i > 50)
        {
            Destroy(this.gameObject);
            playerControl.canCast[spellNum] = true;
        }
    }
}
