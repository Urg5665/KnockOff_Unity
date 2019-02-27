using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindWaveThrow : MonoBehaviour
{
    public float throwSpeed;
    public int playerInt;
    public int spellNum;

    public GameObject player;
    public GameObject playerAim;
    public PlayerControl playerControl;
    public PlayerControlXbox playerControlXbox;

    public float windForce;
    public Vector3  spellDir;

    public int rangeCounter;
    public int maxRange;

    public static bool hitPlayer; // so physics doesnt freek out, this means the first time the player is hit by this is does not fucking anhiliate them - Mark

    private void Awake()
    {
        if (playerInt == 1)
        {
            player = GameObject.Find("Player1");
            playerAim = player.transform.GetChild(0).gameObject;
            playerControl = player.GetComponent<PlayerControl>();
            spellNum = playerControl.spellSelected;
        }
        if (playerInt == 2)
        {
            player = GameObject.Find("Player2");
            playerAim = GameObject.Find("Player2Aim");
            playerControlXbox = player.GetComponent<PlayerControlXbox>();
            spellNum = playerControlXbox.spellSelected;
        }
        maxRange = 10;
        transform.LookAt(playerAim.transform);
        spellDir = this.gameObject.transform.forward;
        windForce = 750;
        hitPlayer = false;
        throwSpeed = 20;
        rangeCounter = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (!hitPlayer && playerInt == 1 && collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(spellDir.normalized * windForce); // Knock Back
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 200); // Knock Up
            Destroy(this.gameObject);
            playerControl.canCast[spellNum] = true;
            hitPlayer = true;
            playerControl.spellPrimary[spellNum] = "";
            playerControl.spellSecondary[spellNum] = ""; // Reset Spell to empty

        }
        if (!hitPlayer && playerInt == 2 && collision.gameObject.tag == "Player1")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(spellDir.normalized * windForce); // Knock Back
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 200); // Knock Up
            Destroy(this.gameObject);
            playerControlXbox.canCast[spellNum] = true;
            hitPlayer = true;
            playerControlXbox.spellPrimary[spellNum] = "";
            playerControlXbox.spellSecondary[spellNum] = ""; // Reset Spell to empty

        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
        rangeCounter++;

        if (rangeCounter > maxRange)
        {
            if (playerInt == 1)
            {
                Destroy(this.gameObject);
                playerControl.canCast[spellNum] = true;
                playerControl.spellPrimary[spellNum] = "";
                playerControl.spellSecondary[spellNum] = ""; // Reset Spell to empty
            }

            if (playerInt == 2)
            {
                Destroy(this.gameObject);
                playerControlXbox.canCast[spellNum] = true;
                playerControlXbox.spellPrimary[spellNum] = "";
                playerControlXbox.spellSecondary[spellNum] = ""; // Reset Spell to empty
            }
        }
    }
}
