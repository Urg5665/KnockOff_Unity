using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallThrow : MonoBehaviour
{
    public float throwSpeed;
    public int playerInt;
    public int spellNum;

    public GameObject player;
    public GameObject playerAim;
    public PlayerControl playerControl;
    public PlayerControlXbox playerControlXbox;

    public bool dashSpell; // This will tell the spell to seek out the oppoentafter a dash// to hard to cast after dashing

    public int rangeCounter;
    public int maxRange;

    public CameraMove cameraMove;

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
        throwSpeed = 30;
        rangeCounter = 0;
        cameraMove = GameObject.Find("MainCamera").GetComponent<CameraMove>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (playerInt == 1 && collision.gameObject.tag == "Player2")
        {
            //collision.gameObject.GetComponent<PlayerControlXbox>().speed = 0;
            collision.gameObject.transform.position = 
                new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 6, collision.gameObject.transform.position.z);
            Destroy(this.gameObject);
           playerControl.canCast[spellNum] = true;
           playerControl.spellPrimary[spellNum] = "";
           playerControl.spellSecondary[spellNum] = ""; // Reset Spell to empty
           StartCoroutine(cameraMove.Shake(.3f, 1f));
        }
        if (playerInt == 2 && collision.gameObject.tag == "Player1")
        {
            collision.gameObject.transform.position =
                new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 6, collision.gameObject.transform.position.z);
            Destroy(this.gameObject);
            playerControlXbox.canCast[spellNum] = true;
            playerControlXbox.spellPrimary[spellNum] = "";
            playerControlXbox.spellSecondary[spellNum] = ""; // Reset Spell to empty
            //StartCoroutine(cameraMove.Shake(.3f, 1f));
        }

    }

    void FixedUpdate()
    {
        if (!dashSpell)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
        }
        if (dashSpell)
        {
            if (playerInt == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player2").transform.position, throwSpeed * Time.deltaTime);
            }
            if (playerInt == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player1").transform.position, throwSpeed * Time.deltaTime);
            }
        }
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

