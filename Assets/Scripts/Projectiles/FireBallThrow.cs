using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallThrow : MonoBehaviour
{
    public float throwSpeed;
    public int playerInt;
    public int spellNum;

    public GameObject player1;
    public GameObject player1Aim;
    public PlayerControl playerControl;

    public int rangeCounter;
    public int maxRange;
    private void Awake()
    {
        maxRange = 10;
        player1 = GameObject.Find("Player1");

        player1Aim = GameObject.Find("Player1Aim");
        playerControl = player1.GetComponent<PlayerControl>();
        spellNum = playerControl.spellSelected;
        transform.LookAt(player1Aim.transform);
        throwSpeed = 30;
        rangeCounter = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ( rangeCounter > 10 && collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Dummy")
        {
            collision.gameObject.transform.position = 
                new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 5, collision.gameObject.transform.position.z);
            Destroy(this.gameObject);
           playerControl.canCast[spellNum] = true;
           playerControl.spellPrimary[spellNum] = "";
           playerControl.spellSecondary[spellNum] = ""; // Reset Spell to empty
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
        rangeCounter++;

        if (rangeCounter > maxRange)
        {
            Destroy(this.gameObject);
            playerControl.canCast[spellNum] = true;
            playerControl.spellPrimary[spellNum] = "";
            playerControl.spellSecondary[spellNum] = ""; // Reset Spell to empty
        }
    }
}
