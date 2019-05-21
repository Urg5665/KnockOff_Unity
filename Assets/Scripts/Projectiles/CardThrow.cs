using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardThrow : MonoBehaviour
{
    public float rotSpeed;
    public float throwSpeed;
    public int playerInt;
    public int cardNum;

    public GameObject player;
    public GameObject playerAim;
    public PlayerControl playerControl;
    public PlayerControlXbox playerControlXbox;

    public BoxCollider cardCollider;

    public string resType;
    public string resType2;

    public bool toRes;
    public bool toPlayer;

    public int rangeCounter;
    public int maxRange;

    public GameObject cardTrail;
    public GameObject cardTrailNew;

    private void Awake()
    {
        toRes = true;
        toPlayer = false;
        rangeCounter = 0;
        if (playerInt == 1)
        {
            player = GameObject.Find("Player1");
            playerAim = player.transform.GetChild(0).gameObject;
            playerControl = player.GetComponent<PlayerControl>();
            cardNum = playerControl.spellSelected;
        }
        else if (playerInt == 2)
        {
            player = GameObject.Find("Player2");
            playerAim = GameObject.Find("Player2Aim");
            playerControlXbox = player.GetComponent<PlayerControlXbox>();
            cardNum = playerControlXbox.spellSelected;
        }

        transform.LookAt(playerAim.transform);
        maxRange = 30; // Just to fit with skill shot // 35 range, 30 speed
        throwSpeed = 40;
        cardCollider = this.GetComponent<BoxCollider>();
        cardCollider.isTrigger = true;
        cardTrailNew = Instantiate(cardTrail, this.transform.position, this.transform.rotation);
        cardTrailNew.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - .25f, this.transform.position.z);
        cardTrailNew.GetComponent<CardTrailThrow>().cardTrailTarget = this.gameObject;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "fireRes" && toRes == true)
        {
            resType = "Fire";
            resType2 = "AOE";
            cardCollider.isTrigger = false;
            toRes = false;
            toPlayer = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "windRes" && toRes == true)
        {
            resType = "Wind";
            resType2 = "Range";
            cardCollider.isTrigger = false;
            toRes = false;
            toPlayer = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "waterRes" && toRes == true)
        {
            resType = "Water";
            resType2 = "Dash";
            cardCollider.isTrigger = false;
            toRes = false;
            toPlayer = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "earthRes" && toRes == true)
        {
            resType = "Earth";
            //Debug.Log("Boom Picked");
            resType2 = "Boom";
            cardCollider.isTrigger = false;
            toRes = false;
            toPlayer = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player1" && toPlayer && playerInt == 1)
        {
            if (playerControl.spellPrimary[cardNum] == "")
            {
                playerControl.spellPrimary[cardNum] = resType;
            }
            else if (playerControl.spellPrimary[cardNum] != "")
            {
                playerControl.spellSecondary[cardNum] = resType2;
            }
            playerControl.canCast[cardNum] = true;
            playerControl.cardsThrown--;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player2" && toPlayer && playerInt == 2)
        {
            if (playerControlXbox.spellPrimary[cardNum] == "")
            {
                playerControlXbox.spellPrimary[cardNum] = resType;
            }
            else if (playerControlXbox.spellPrimary[cardNum] != "")
            {
                playerControlXbox.spellSecondary[cardNum] = resType2;
            }
            playerControlXbox.canCast[cardNum] = true;
            playerControlXbox.cardsThrown--;
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        //Debug.Log(Vector3.Distance(this.transform.position, player.transform.position));
        rangeCounter++;

        if (rangeCounter == maxRange && toRes) // miss
        {
            Destroy(this.gameObject);
            if (playerInt == 1)
            {
                playerControl.canCast[cardNum] = true;
                playerControl.cardsThrown--;
            }
            if (playerInt == 2)
            {
                playerControlXbox.canCast[cardNum] = true;
                playerControlXbox.cardsThrown--;
            }
        }

        if (toRes)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
            //rangeCounter++;
        }

        if (toPlayer)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed, Space.World);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1.5f * throwSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, this.transform.localRotation.y, 0);
            cardCollider.isTrigger = true;
        }
        if (toPlayer && rangeCounter > maxRange * 0.5f && Vector3.Distance(this.transform.position, player.transform.position) < 1)  // Fixing Card on player bug
        {
            //Debug.Log("bugFixed cardeffect given");
            transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed, Space.World);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, throwSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, this.transform.localRotation.y, 0);
            cardCollider.isTrigger = true;
            Destroy(this.gameObject);
            if (playerInt == 1)
            {
                playerControl.canCast[cardNum] = true;
                playerControl.cardsThrown--;
                if (playerControl.spellPrimary[cardNum] == "")
                {
                    playerControl.spellPrimary[cardNum] = resType;
                }
                else if (playerControl.spellPrimary[cardNum] != "")
                {
                    playerControl.spellSecondary[cardNum] = resType2;
                }
            }
            if (playerInt == 2)
            {
                playerControlXbox.canCast[cardNum] = true;
                playerControlXbox.cardsThrown--;
                if (playerControlXbox.spellPrimary[cardNum] == "")
                {
                    playerControlXbox.spellPrimary[cardNum] = resType;
                }
                else if (playerControlXbox.spellPrimary[cardNum] != "")
                {
                    playerControlXbox.spellSecondary[cardNum] = resType2;
                }
            }
        }

    }
}



