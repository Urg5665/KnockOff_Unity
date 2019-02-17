using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardThrow : MonoBehaviour
{
    public float rotSpeed;
    public float throwSpeed;
    public int playerInt;
    public int cardNum;

    public GameObject player1;
    public GameObject player1Aim;
    public PlayerControl playerControl;

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
        player1 = GameObject.Find("Player1");
        player1Aim = GameObject.Find("Player1Aim");
        transform.LookAt(player1Aim.transform);
        playerControl = player1.GetComponent<PlayerControl>();
        cardNum = playerControl.spellSelected;
        maxRange = 60;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "fireRes" && toRes == true)
        {
            toRes = false;
            toPlayer = true;
            resType = "Fire";
            resType2 = "AOE";
            rangeCounter = maxRange + 1; ;
        }
        if (collision.gameObject.tag == "windRes" && toRes == true)
        {
            toRes = false;
            toPlayer = true;
            resType = "Wind";
            resType2 = "Range";
            rangeCounter = maxRange +1 ;
        }
        if (collision.gameObject.tag == "Target")
        {
            toRes = false;
            toPlayer = true;
            rangeCounter = maxRange+1;
        }

        if (collision.gameObject.tag == "Player1" && rangeCounter > maxRange)
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
        }
    }

    void FixedUpdate()
    {
        if (toRes)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * throwSpeed, Space.Self);
            rangeCounter++;
        }

        if (toPlayer)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed, Space.World);
            transform.position = Vector3.MoveTowards(transform.position, player1.transform.position, throwSpeed * Time.deltaTime);
        }
        if (rangeCounter > maxRange)
        {
            toRes = false;
            toPlayer = true;
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

 * 
 */

