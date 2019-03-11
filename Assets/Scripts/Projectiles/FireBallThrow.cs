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
        cameraMove = GameObject.Find("MainCamera").GetComponent<CameraMove>();
        hitSlow = 101;
        audioClip = this.GetComponent<AudioSource>().clip;
        audioSource = this.GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (playerInt == 1 && collision.gameObject.tag == "Player2")
        {
            //collision.gameObject.GetComponent<PlayerControlXbox>().speed = 0;
            
            StartCoroutine(cameraMove.Shake(.3f, .5f));
            // Kill
            //collision.gameObject.transform.position = 
             //   new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 6, collision.gameObject.transform.position.z);
            // Stun
            collision.gameObject.GetComponent<PlayerControlXbox>().speed = 0;
            collision.gameObject.GetComponent<PlayerControlXbox>().stunLength = 100;
            playerControl.canCast[spellNum] = true;
           playerControl.spellPrimary[spellNum] = "";
           playerControl.spellSecondary[spellNum] = ""; // Reset Spell to empty
           hitEffectInGame = Instantiate(hitEffect);
           //hitEffectInGame.transform.position = this.transform.position;
           hitEffectInGame.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
           hitSlow = 0;
        }
        if (playerInt == 2 && collision.gameObject.tag == "Player1")
        {
            StartCoroutine(cameraMove.Shake(.3f, .5f));
            //collision.gameObject.transform.position =
            //    new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 6, collision.gameObject.transform.position.z);
            // Stun
            collision.gameObject.GetComponent<PlayerControl>().speed = 0;
            collision.gameObject.GetComponent<PlayerControl>().stunLength = 100;
            playerControlXbox.canCast[spellNum] = true;
            playerControlXbox.spellPrimary[spellNum] = "";
            playerControlXbox.spellSecondary[spellNum] = ""; // Reset Spell to empty
            hitEffectInGame = Instantiate(hitEffect);
            //hitEffectInGame.transform.position = this.transform.position;
            hitEffectInGame.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            hitSlow = 0;

        }

    }

    void FixedUpdate()
    {
        if (hitSlow == 0)
        {
            //Time.timeScale = 0.2f;
            Time.timeScale = 1.0f;
            hitSlow++;
        }
        if (hitSlow <= 10)
        {
            hitSlow++;
        }
        if (hitSlow == 10 && !audioSource.isPlaying)
        {
            Time.timeScale = 1.0f;
            Destroy(this.gameObject);
        }


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

