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
    public GameObject dashTarget;

    public bool dashSpell; // This will tell the spell to seek out the oppoentafter a dash// to hard to cast after dashing


    public float windForce;
    public float windKnockUp;
    public Vector3  spellDir;

    public int rangeCounter;
    public int maxRange;

    public Quaternion initialRotation;

    public static bool hitPlayer; // so physics doesnt freek out, this means the first time the player is hit by this is does not fucking anhiliate them - Mark

    public int hitSlow; // For Effects i guess?

    public CameraMove cameraMove;
    private void Awake()
    {
        if (playerInt == 1)
        {
            player = GameObject.Find("Player1");
            playerAim = player.transform.GetChild(0).gameObject;
            playerControl = player.GetComponent<PlayerControl>();
            spellNum = playerControl.spellSelected;
            dashTarget = GameObject.Find("Player2");
        }
        if (playerInt == 2)
        {
            player = GameObject.Find("Player2");
            playerAim = GameObject.Find("Player2Aim");
            playerControlXbox = player.GetComponent<PlayerControlXbox>();
            spellNum = playerControlXbox.spellSelected;
            dashTarget = GameObject.Find("Player1");
        }
        maxRange = 10;

        transform.LookAt(playerAim.transform);
        spellDir = this.gameObject.transform.forward;
        windForce = 600;
        windKnockUp = 400;
        hitPlayer = false;
        throwSpeed = 30;
        rangeCounter = 0;
        initialRotation = this.transform.rotation;
        hitSlow = 101;
        cameraMove = GameObject.Find("MainCamera").GetComponent<CameraMove>();
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (!hitPlayer && playerInt == 1 && collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * windForce); // Knock Back
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * windKnockUp); // Knock Up
            //Destroy(this.gameObject);
            playerControl.canCast[spellNum] = true;
            hitPlayer = true;
            playerControl.spellPrimary[spellNum] = "";
            playerControl.spellSecondary[spellNum] = ""; // Reset Spell to empty
            hitSlow = 0;
            StartCoroutine(cameraMove.Shake(.3f, .5f));
            //Camera.main.fieldOfView -= 5;

        }
        if (!hitPlayer && playerInt == 2 && collision.gameObject.tag == "Player1")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * windForce); // Knock Back
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * windKnockUp); // Knock Up
            playerControlXbox.canCast[spellNum] = true;
            hitPlayer = true;
            playerControlXbox.spellPrimary[spellNum] = "";
            playerControlXbox.spellSecondary[spellNum] = ""; // Reset Spell to empty
            hitSlow = 0;
            StartCoroutine(cameraMove.Shake(.3f, .5f));

        }

    }

    void FixedUpdate()
    {
        if (hitSlow == 0)
        {
            Time.timeScale = 0.2f;
            hitSlow++;
        }
        if (hitSlow <= 10)
        {
            hitSlow++;
        }
        if (hitSlow == 10)
        {
            Time.timeScale = 1.0f;
            Destroy(this.gameObject);
        }



        if (dashSpell)
        {
            transform.LookAt(dashTarget.transform);
        }
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
/*
 * if (!dashSpell)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
        }
        if (dashSpell)
        {
            if (playerInt == 1)
            {
                playerAim.transform.position = GameObject.Find("Player2").transform.position;
                //transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);

                //transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player2").transform.position, throwSpeed * Time.deltaTime);

                transform.LookAt(GameObject.Find("Player2").transform.position,Vector3.up);
                transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
                transform.position = new Vector3(transform.position.x, 2.75f, transform.position.z);
            }
            if (playerInt == 2)
            {
                //transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player1").transform.position, throwSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
            }
        }
*/