using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
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

    public int cardsThrown;
    public float slowDownPerCard = 2.5f;

    public GameObject newSpell;

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
        if ( this.transform.position.y < 2.5f)
        {
            grounded = false;
        }
        if (this.transform.position.y >= 2.5f)
        {
            grounded = true;
        }


    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Card" && collision.GetComponent<CardThrow>().i > 100)
        {
            cardsThrown--;
            speed = speed + slowDownPerCard; // slow aplied for each card in play
            canCast[collision.GetComponent<CardThrow>().cardNum] = true;
            //Debug.Log(spellPrimary[collision.GetComponent<CardThrow>().cardNum]);
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
            newSpell = Instantiate(spellProjectile[0], this.transform.position, card.transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<FireBallThrow>().spellNum = spellSelected;
            canCast[spellSelected] = false;
        }
        else if (spellSecondary[spellSelected] == "AOE")
        {
            newSpell = Instantiate(spellProjectile[0], this.transform.position, card.transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<FireBallThrow>().spellNum = spellSelected;
            Debug.Log("Did AOE");
            canCast[spellSelected] = false;
        }
        else if (spellSecondary[spellSelected] == "Range")
        {
            newSpell = Instantiate(spellProjectile[0], this.transform.position, card.transform.rotation);
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
            newSpell = Instantiate(spellProjectile[1], this.transform.position, card.transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WindWaveThrow>().spellNum = spellSelected;
            //Debug.Log("WindWave" + (spellSelected + 1) + " Thrown");
            canCast[spellSelected] = false;
        }
        else if (spellSecondary[spellSelected] == "AOE")
        {
            newSpell = Instantiate(spellProjectile[1], this.transform.position, card.transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WindWaveThrow>().spellNum = spellSelected;
            Debug.Log("Did AOE");
            canCast[spellSelected] = false;
        }
        else if (spellSecondary[spellSelected] == "Range")
        {
            newSpell = Instantiate(spellProjectile[1], this.transform.position, card.transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WindWaveThrow>().spellNum = spellSelected;
            newSpell.GetComponent<WindWaveThrow>().maxRange = 50;
            canCast[spellSelected] = false;
        }

    }




}
 

