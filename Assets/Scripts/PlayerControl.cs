﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public GameObject player1Aim;
    public int playerNum;
    public float speed;

    public Transform movement;
    public Rigidbody rb;

    public bool grounded;

    public GameObject[] spellProjectile; // The actual Fireball, air block, earth wall
    public int spellSelected = 1;
    public bool[] canCast;

    public string[] spellPrimary; // Keywords "Fire", "Wind", "Earth" "Water" use "" for empty
    public string[] spellSecondary; // Keywords "Aoe", "Range", "Lob"? not as sure about the last two use "" for empty

    public GameObject card;
    public GameObject newCard;
    public GameObject cardTrail;
    public GameObject newCardTrail;
    public Transform AOEpoint;

    public int cardsThrown;
    public float slowDownPerCard = 2.5f;

    public GameObject newSpell;
    public GameObject[] newSpellAOE;

    void Start()
    {
        grounded = true;
        cardsThrown = 0;
        canCast = new bool[4]; // ignore zero here
        for(int i = 0; i < 4; i++)
        {
            canCast[i] = true;
            spellPrimary[i] = "";
            spellSecondary[i] = "";
        }
        //Time.timeScale = 0.4f;
    }
   
    void Update()
    {
        
        // Spell selection
        if (Input.GetKey(KeyCode.Alpha1))
            spellSelected = 0;
        if (Input.GetKey(KeyCode.Alpha2))
            spellSelected = 1;
        if (Input.GetKey(KeyCode.Alpha3))
            spellSelected = 2;
        if (Input.GetKey(KeyCode.Alpha4))
            spellSelected = 3;

        if (grounded) // movement
        {
            //float moveHL = Input.GetAxis("Horizontal") * speed; // Sorry I fucked with this, 
            //float moveVL = Input.GetAxis("Vertical") * speed; // I will definity come back to controller combabtabilty, but pls just use keyboard for now - Mark

            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.S))
                transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);

            //moveHL *= Time.deltaTime;
            //moveVL *= Time.deltaTime;

            //movement.Translate(moveVL,0, moveHL, Space.World);

            // Card Casting Commands
            if (Input.GetMouseButtonDown(0) && cardsThrown < 4 && canCast[spellSelected] && spellSecondary[spellSelected] == "" ) // Shoot Card
            {
                CardGather();
            }
            if (Input.GetMouseButtonDown(0) && cardsThrown < 4 && canCast[spellSelected] && spellSecondary[spellSelected] != "")  // Disabel Shooitng Card because spell is maxed
            {
                Debug.Log("Spell Maxed - Cast it!");
            }


            // Spell Casting Commands
            if (Input.GetMouseButtonDown(1) && cardsThrown < 4 && canCast[spellSelected] && spellPrimary[spellSelected] == "") // You Have no Spell
            {
                Debug.Log("No Spell Avaliable");
            }
            if (Input.GetMouseButtonDown(1) && cardsThrown < 4 && canCast[spellSelected] && spellPrimary[spellSelected] == "Fire") // Shoot Fireball
            {
                Fireball();
            }
            if (Input.GetMouseButtonDown(1) && cardsThrown < 4 && canCast[spellSelected] && spellPrimary[spellSelected] == "Wind") // Shoot Wind Knock
            {
                WindKnockback();
            }

        }   
        if ( this.transform.position.y < 2.5f || this.transform.position.y > 3f)
        {
            grounded = false;
        }
        if (this.transform.position.y >= 2.5f && this.transform.position.y <= 3f)
        {
            grounded = true;
        }


    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Card" && collision.GetComponent<CardThrow>().rangeCounter > collision.GetComponent<CardThrow>().maxRange)
        {
            cardsThrown--;
            speed = speed + slowDownPerCard; // slow aplied for each card in play
            canCast[collision.GetComponent<CardThrow>().cardNum] = true;
            //Debug.Log(spellPrimary[collision.GetComponent<CardThrow>().cardNum]);
        }
        if (collision.gameObject.tag == "fireRes" || collision.gameObject.tag == "windRes")
        {
            speed = -1 * speed; // Keep Player From Bouncing off Resources
        }
    }
    private void CardGather()
    {
        newCard = Instantiate(card, this.transform.position, card.transform.rotation);
        newCard.transform.position = new Vector3(newCard.transform.position.x, newCard.transform.position.y - .25f, newCard.transform.position.z);
        newCard.GetComponent<CardThrow>().cardNum = spellSelected;
        cardsThrown++;
        speed = speed - slowDownPerCard; // slow aplied for each card in play

        newCardTrail = Instantiate(cardTrail, this.transform.position, card.transform.rotation);
        newCardTrail.transform.position = new Vector3(newCard.transform.position.x, newCard.transform.position.y - .25f, newCard.transform.position.z);
        newCardTrail.GetComponent<CardTrailThrow>().cardTrailTarget = newCard;
        canCast[spellSelected] = false;
    }
    private void Fireball()
    {
       if (spellSecondary[spellSelected] == "")
        {
            newSpell = Instantiate(spellProjectile[0], this.transform.position, spellProjectile[0].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<FireBallThrow>().spellNum = spellSelected;
            //Debug.Log("Basic");
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "AOE")
        {
            for (int i = 0; i < 8; i++)
            {
                newSpellAOE[i] = Instantiate(spellProjectile[0], this.transform.position, spellProjectile[0].transform.rotation);
                newSpellAOE[i].transform.position = new Vector3(newSpellAOE[i].transform.position.x, newSpellAOE[i].transform.position.y - .25f, newSpellAOE[i].transform.position.z);
                newSpellAOE[i].GetComponent<FireBallThrow>().spellNum = spellSelected;
                // I got Really Really Fucking Lazy and Hard Coded the Draw Cricle about point function to make this work. 
                //Im ashamed of the following code and wil fix when i figrue out abetter draw circle - Mark
                if (i == 0)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z);
                }
                if (i == 1)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x + 7, this.transform.position.y, this.transform.position.z + 7);
                }
                if (i == 2)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 10);
                }
                if (i == 3)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x - 7, this.transform.position.y, this.transform.position.z + 7);
                }
                if (i == 4)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x - 10, this.transform.position.y, this.transform.position.z);
                }
                if (i == 5)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x - 7, this.transform.position.y, this.transform.position.z - 7);
                }
                if (i == 6)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 10);
                }
                if (i == 7)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x + 7, this.transform.position.y, this.transform.position.z - 7);
                }
                newSpellAOE[i].GetComponent<FireBallThrow>().transform.LookAt(AOEpoint); 
            }
        }
        if (spellSecondary[spellSelected] == "Range")
        {
            newSpell = Instantiate(spellProjectile[0], this.transform.position, spellProjectile[0].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<FireBallThrow>().spellNum = spellSelected;
            newSpell.GetComponent<FireBallThrow>().maxRange = 50;
            canCast[spellSelected] = false;
        }




    }
    private void WindKnockback()
    {

        if (spellSecondary[spellSelected] == "")
        {
            newSpell = Instantiate(spellProjectile[1], this.transform.position, spellProjectile[1].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WindWaveThrow>().spellNum = spellSelected;
            //Debug.Log("WindWave" + (spellSelected + 1) + " Thrown");
            canCast[spellSelected] = false;
        }
        else if (spellSecondary[spellSelected] == "AOE")
        {
            for (int i = 0; i < 8; i++)
            {
                newSpellAOE[i] = Instantiate(spellProjectile[1], this.transform.position, spellProjectile[0].transform.rotation);
                newSpellAOE[i].transform.position = new Vector3(newSpellAOE[i].transform.position.x, newSpellAOE[i].transform.position.y - .25f, newSpellAOE[i].transform.position.z);
                newSpellAOE[i].GetComponent<WindWaveThrow>().spellNum = spellSelected;
                // I got Really Really Fucking Lazy and Hard Coded the Draw Cricle about point function to make this work. 
                //Im ashamed of the following code and wil fix when i figrue out abetter draw circle - Mark
                if (i == 0)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z);
                }
                if (i == 1)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x + 7, this.transform.position.y, this.transform.position.z + 7);
                }
                if (i == 2)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 10);
                }
                if (i == 3)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x - 7, this.transform.position.y, this.transform.position.z + 7);
                }
                if (i == 4)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x - 10, this.transform.position.y, this.transform.position.z);
                }
                if (i == 5)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x - 7, this.transform.position.y, this.transform.position.z - 7);
                }
                if (i == 6)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 10);
                }
                if (i == 7)
                {
                    AOEpoint.position = new Vector3(this.transform.position.x + 7, this.transform.position.y, this.transform.position.z - 7);
                }
                newSpellAOE[i].GetComponent<WindWaveThrow>().transform.LookAt(AOEpoint);
            }
        }
        else if (spellSecondary[spellSelected] == "Range")
        {
            newSpell = Instantiate(spellProjectile[1], this.transform.position, spellProjectile[1].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WindWaveThrow>().spellNum = spellSelected;
            newSpell.GetComponent<WindWaveThrow>().maxRange = 50;
            canCast[spellSelected] = false;
        }

    }



}
/*
 * 
 * 
 *  for(int i = 0; i > 8; i++)
           {
               newSpell = Instantiate(spellProjectile[0], this.transform.position, spellProjectile[0].transform.rotation);
               newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
               newSpell.GetComponent<FireBallThrow>().spellNum = spellSelected;
               // I got Really Really Fucking Lazy and Hard Coded the Draw Cricle about point function to make this work. 
               //Im ashamed of the following code and wil fix when i figrue out abetter draw circle - Mark
               if (i == 0)
               {
                   AOEpoint.position = new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z);
               }
               if (i == 1)
               {
                   AOEpoint.position = new Vector3(this.transform.position.x + 7, this.transform.position.y, this.transform.position.z + 7);
               }
               if (i == 2)
               {
                   AOEpoint.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 10);
               }
               if (i == 3)
               {
                   AOEpoint.position = new Vector3(this.transform.position.x - 7, this.transform.position.y, this.transform.position.z + 7);
               }
               if (i == 4)
               {
                   AOEpoint.position = new Vector3(this.transform.position.x -10, this.transform.position.y, this.transform.position.z);
               }
               if (i == 5)
               {
                   AOEpoint.position = new Vector3(this.transform.position.x -7, this.transform.position.y, this.transform.position.z - 7);
               }
               if (i == 6)
               {
                   AOEpoint.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 10);
               }
               if (i == 7)
               {
                   AOEpoint.position = new Vector3(this.transform.position.x + 7, this.transform.position.y, this.transform.position.z - 7);
               }
               newSpell.GetComponent<FireBallThrow>().transform.LookAt(AOEpoint);
               Debug.Log(i);
           }*/

