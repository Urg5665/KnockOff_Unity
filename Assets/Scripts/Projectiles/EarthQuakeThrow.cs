using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeThrow : MonoBehaviour
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

    //public CameraMove cameraMove;

    public Vector3 dashTarget;

    public GameObject hitEffect;
    public GameObject hitEffectInGame;

    public int hitSlow; // For Effects i guess?

    public AudioClip audioClip;
    public AudioSource audioSource;

    private void Awake()
    {
        if (playerInt == 1)
        {
            player = GameObject.Find("Player1");
            playerAim = player.transform.GetChild(0).gameObject;
            playerControl = player.GetComponent<PlayerControl>();
            spellNum = playerControl.spellSelected;
            dashTarget = GameObject.Find("Player2").transform.position;
            dashTarget = new Vector3(dashTarget.x, dashTarget.y - 5f, dashTarget.z);
        }
        if (playerInt == 2)
        {
            player = GameObject.Find("Player2");
            playerAim = GameObject.Find("Player2Aim");
            playerControlXbox = player.GetComponent<PlayerControlXbox>();
            spellNum = playerControlXbox.spellSelected;
            dashTarget = GameObject.Find("Player1").transform.position;
        }

        maxRange = 10;
        transform.LookAt(playerAim.transform);
        throwSpeed = 30;
        rangeCounter = 0;
        //cameraMove = GameObject.Find("MainCamera").GetComponent<CameraMove>();
        hitSlow = 101;
        audioClip = this.GetComponent<AudioSource>().clip;
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            Debug.Log(collision.gameObject.name);
            //collision.gameObject.GetComponentInParent<TileBehavoir>().destroyed = true;
        }
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponentInParent<TileBehavoir>().destroyed = true;
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
                transform.position = Vector3.MoveTowards(transform.position, dashTarget, throwSpeed * Time.deltaTime);
            }
            if (playerInt == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, dashTarget, throwSpeed * Time.deltaTime);
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
