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

    public float windForce;
    public Vector3  spellDir;

    public int rangeCounter;
    public int maxRange;

    public static bool hitPlayer; // so physics doesnt freek out, this means the first time the player is hit by this is does not fucking anhiliate them - Mark

    private void Awake()
    {
        maxRange = 10;
        player1 = GameObject.Find("Player1");
        player1Aim = GameObject.Find("Player1Aim");
        transform.LookAt(player1Aim.transform);
        playerControl = player1.GetComponent<PlayerControl>();
        spellNum = playerControl.spellSelected;
        spellDir = this.gameObject.transform.forward;
        windForce = 750;
        hitPlayer = false;
        throwSpeed = 20;
        rangeCounter = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (!hitPlayer && rangeCounter > 10 && collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Dummy")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(spellDir.normalized * windForce); // Knock Back
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 200); // Knock Up
            Destroy(this.gameObject);
            playerControl.canCast[spellNum] = true;
            hitPlayer = true;
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
            playerControl.spellSecondary[spellNum] = ""; // Reset Spell to Empty
        }
    }
}
