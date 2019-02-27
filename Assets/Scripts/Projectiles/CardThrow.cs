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
        maxRange = 60;
        cardCollider = this.GetComponent<BoxCollider>();
        cardCollider.isTrigger = true;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "fireRes" && toRes == true)
        {
            resType = "Fire";
            resType2 = "AOE";
            rangeCounter = maxRange - 1; ;
            cardCollider.isTrigger = false;
        }
        if (collision.gameObject.tag == "windRes" && toRes == true)
        {
            resType = "Wind";
            resType2 = "Range";
            rangeCounter = maxRange -1 ;
            cardCollider.isTrigger = false;
        }
        if (collision.gameObject.tag == "waterRes" && toRes == true)
        {
            resType = "Water";
            resType2 = "Dash";
            rangeCounter = maxRange - 1;
            cardCollider.isTrigger = false;
        }
        if (collision.gameObject.tag == "Target")
        {
            toRes = false;
            toPlayer = true;
            rangeCounter = maxRange+1;
        }

        if (collision.gameObject.tag == "Player1" && rangeCounter > maxRange && playerInt == 1)
        {
            if (playerControl.spellPrimary[cardNum] == "")
            {
                playerControl.spellPrimary[cardNum] = resType;
                //Debug.Log("Primary:" + playerControl.spellPrimary[cardNum] + "  Secondary:" + playerControl.spellSecondary[cardNum]);
            }
            else if (playerControl.spellPrimary[cardNum] != "")
            {
                playerControl.spellSecondary[cardNum] = resType2;
                //Debug.Log("Primary:" + playerControl.spellPrimary[cardNum] + "  Secondary:" + playerControl.spellSecondary[cardNum]);
            }
            Destroy(this.gameObject);
            //Debug.Log("Card Hit Player");
        }
        if (collision.gameObject.tag == "Player2" && rangeCounter > maxRange && playerInt == 2)
        {
            if (playerControlXbox.spellPrimary[cardNum] == "")
            {
                playerControlXbox.spellPrimary[cardNum] = resType;
                Debug.Log("Primary:" + playerControlXbox.spellPrimary[cardNum] + "  Secondary:" + playerControlXbox.spellSecondary[cardNum]);
            }
            else if (playerControlXbox.spellPrimary[cardNum] != "")
            {
                playerControlXbox.spellSecondary[cardNum] = resType2;
                Debug.Log("Primary:" + playerControlXbox.spellPrimary[cardNum] + "  Secondary:" + playerControlXbox.spellSecondary[cardNum]);
            }
            Destroy(this.gameObject);
            Debug.Log("Card Hit Player 2");
        }
    }

    void FixedUpdate()
    {
        if (rangeCounter == maxRange + 1)
        {
            toRes = false;
            toPlayer = true;
            transform.rotation = Quaternion.Euler(0, this.transform.localRotation.y, 0);
            cardCollider.isTrigger = true;
            //Debug.Log("Quat Changed");
            rangeCounter++;           
        }

        if (toRes)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
            rangeCounter++;
        }

        if (toPlayer)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed, Space.World);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, throwSpeed * Time.deltaTime);
        }



    }

}
/*        if (Input.GetMouseButtonDown(0))
        {
            GameObject targetSave = Instantiate(cardTarget);
            targetSave.transform.position = this.transform.position;
            allTargets[cardsThrown] = targetSave;
            cardsThrown++;

        }

 *         // Cheeck Card Based on player didstnace
         if (rangeCounter > maxRange && Mathf.Abs(this.transform.position.x - player1.transform.position.x) <= 1  && Mathf.Abs(this.transform.position.z - player1.transform.position.z) <= 1)
        {
            if (playerControl.spellPrimary[cardNum] == "")
            {
                playerControl.spellPrimary[cardNum] = resType;
                Debug.Log("Primary:" + playerControl.spellPrimary[cardNum] + "  Secondary:" + playerControl.spellSecondary[cardNum]);
            }
            else if (playerControl.spellPrimary[cardNum] != "")
            {
                playerControl.spellSecondary[cardNum] = resType2;
                Debug.Log("Primary:" + playerControl.spellPrimary[cardNum] + "  Secondary:" + playerControl.spellSecondary[cardNum]);
            }
            Destroy(this.gameObject);
            Debug.Log("Card Came Within Range");
            playerControl.cardsThrown--;
            playerControl.speed = playerControl.speed + playerControl.slowDownPerCard; // slow aplied for each card in play
            playerControl.canCast[cardNum] = true;
        }
 */

