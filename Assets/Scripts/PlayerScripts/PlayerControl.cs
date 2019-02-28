using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject player1Aim;
    public PlayerAim playerAim;

    public int playerNum;
    public float speed;
    public float maxSpeed = 10;

    public Transform movement;
    public Rigidbody rb;

    public bool grounded;
    public bool touchingWall;

    public GameObject[] spellProjectile; // The actual Fireball, air block, earth wall
    public int spellSelected;
    public bool[] canCast;

    public GameObject[] onPlayerUIButton;
    //public PointerEventData pointerEvent;

    public string[] spellPrimary; // Keywords "Fire", "Wind", "Earth" "Water" use "" for empty
    public string[] spellSecondary; // Keywords "Aoe", "Range", "Lob"? not as sure about the last two use "" for empty

    public GameObject card;
    public GameObject newCard;
    public GameObject cardTrail;
    public GameObject newCardTrail;
    public Transform AOEpoint;

    public int dashDirection; // This is to keep player locked when dashing
    public int dashDirectionTime;
    public Vector3  dashAim;
    public float waterDashForce;

    public int cardsThrown;
    public float slowDownPerCard = 2.5f;

    public GameObject newSpell;
    public GameObject[] newSpellAOE;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        grounded = true;
        touchingWall = false;
        cardsThrown = 0;
        canCast = new bool[4]; // ignore zero here
        //onPlayerUIButton = new GameObject[4];
        waterDashForce = 400;
        dashDirectionTime = 0;

        for (int i = 0; i < 4; i++)
        {
            canCast[i] = true;
            spellPrimary[i] = "";
            spellSecondary[i] = "";
        }
        slowDownPerCard = 2.5f;
    }

    public void pickDirection()
    {
        spellSelected = playerAim.GetComponent<PlayerAim>().spellSelected;
    }

    void Update()
    {
        pickDirection();
        dashDirectionTime--;

        speed = maxSpeed - (slowDownPerCard * cardsThrown); // apply slow for each card in play
                                                            //Debug.Log("speed" + speed);

        if (dashDirectionTime < 75 && dashDirectionTime > 1) // being dash
        {
            grounded = false;
            transform.position = Vector3.Lerp(transform.position, dashAim, Time.deltaTime);
        }

        if (canCast[dashDirection] && dashDirectionTime < 1) // result invulnerbaility after dash is complete
        {
            this.GetComponent<BoxCollider>().enabled = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            //Debug.Log("Invulnrble Dash Reset");
            this.transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        // Card Casting Commands
        if (Input.GetMouseButtonDown(0) && cardsThrown < 4 && canCast[spellSelected] && spellSecondary[spellSelected] == "") // Shoot Card
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
        if (Input.GetMouseButtonDown(1) && cardsThrown < 4 && canCast[spellSelected] && spellPrimary[spellSelected] == "Water") // Shoot Wind Knock
        {
            WaterPull();
        }
        if (grounded) // movement
        {
            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.S))
                transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);


        }
        if (this.transform.position.y < 2.5f || this.transform.position.y > 3f)
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
            canCast[collision.GetComponent<CardThrow>().cardNum] = true;
        }

    }
    private void CardGather()
    {
        newCard = Instantiate(card, this.transform.position, card.transform.rotation);
        newCard.transform.position = new Vector3(newCard.transform.position.x, newCard.transform.position.y - .25f, newCard.transform.position.z);
        newCard.GetComponent<CardThrow>().cardNum = spellSelected;
        cardsThrown++;
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
            newSpell.GetComponent<FireBallThrow>().maxRange = 25;
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "AOE")
        {
            for (int i = 0; i < 5; i++)
            {
                newSpellAOE[i] = Instantiate(spellProjectile[0], this.transform.position, spellProjectile[0].transform.rotation);
                newSpellAOE[i].transform.position = new Vector3(newSpellAOE[i].transform.position.x, newSpellAOE[i].transform.position.y - .25f, newSpellAOE[i].transform.position.z);
                newSpellAOE[i].GetComponent<FireBallThrow>().spellNum = spellSelected;
                newSpellAOE[i].GetComponent<FireBallThrow>().maxRange = 15;
                // I got Really Really Fucking Lazy and Hard Coded the Draw Cricle about point function to make this work. 
                //Im ashamed of the following code and wil fix when i figrue out abetter draw circle - Mark
                if (i == 0)
                {
                    AOEpoint.position = player1Aim.transform.position;
                }
                if (spellSelected == 0 || spellSelected == 2)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x + 3, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x + 3, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 3)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x - 9, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 4)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x - 3, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                }
                if (spellSelected == 1 || spellSelected == 3)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z + 3);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z + 3);
                    }
                    if (i == 3)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z - 9);
                    }
                    if (i == 4)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z - 3);
                    }
                }

                newSpellAOE[i].GetComponent<FireBallThrow>().transform.LookAt(AOEpoint);
            }
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "Range")
        {
            newSpell = Instantiate(spellProjectile[0], this.transform.position, spellProjectile[0].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<FireBallThrow>().spellNum = spellSelected;
            newSpell.GetComponent<FireBallThrow>().maxRange = 75;
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "Dash")
        {
            newSpell = Instantiate(spellProjectile[0], this.transform.position, spellProjectile[0].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<FireBallThrow>().spellNum = spellSelected;
            newSpell.GetComponent<FireBallThrow>().maxRange = 30;
            newSpell.GetComponent<FireBallThrow>().throwSpeed = 35;
            canCast[spellSelected] = false;
            dashDirection = spellSelected;
            dashAim = new Vector3(player1Aim.transform.position.x , player1Aim.transform.position.y, player1Aim.transform.position.z);
            dashDirectionTime = 75;
            if (this.transform.position.y < 2.5)
            {
                rb.AddForce(Vector3.up * 750);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }

            this.GetComponent<BoxCollider>().enabled = false;
            //Debug.Log("Invulnrble Dash");
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
            newSpell.GetComponent<WindWaveThrow>().maxRange = 25;
        }
        else if (spellSecondary[spellSelected] == "AOE")
        {
            for (int i = 0; i < 8; i++)
            {
                newSpellAOE[i] = Instantiate(spellProjectile[1], this.transform.position, spellProjectile[0].transform.rotation);
                newSpellAOE[i].transform.position = new Vector3(newSpellAOE[i].transform.position.x, newSpellAOE[i].transform.position.y - .25f, newSpellAOE[i].transform.position.z);
                newSpellAOE[i].GetComponent<WindWaveThrow>().spellNum = spellSelected;
                newSpellAOE[i].GetComponent<WindWaveThrow>().maxRange = 25;
                // I got Really Really Fucking Lazy and Hard Coded the Draw Cricle about point function to make this work. 
                //Im ashamed of the following code and wil fix when i figrue out abetter draw circle - Mark
                if (i == 0)
                {
                    AOEpoint.position = player1Aim.transform.position;
                }
                if (spellSelected == 0 || spellSelected == 2)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x + 3, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x + 3, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 3)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x - 9, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 4)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x - 3, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                }
                if (spellSelected == 1 || spellSelected == 3)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z + 3);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z + 3);
                    }
                    if (i == 3)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z - 9);
                    }
                    if (i == 4)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z - 3);
                    }

                }
                newSpellAOE[i].GetComponent<WindWaveThrow>().transform.LookAt(AOEpoint);
            }
            canCast[spellSelected] = false;
        }
        else if (spellSecondary[spellSelected] == "Range")
        {
            newSpell = Instantiate(spellProjectile[1], this.transform.position, spellProjectile[1].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WindWaveThrow>().spellNum = spellSelected;
            newSpell.GetComponent<WindWaveThrow>().maxRange = 75;
            newSpell.GetComponent<WindWaveThrow>().throwSpeed = 25;
            canCast[spellSelected] = false;
        }
        else if (spellSecondary[spellSelected] == "Dash")
        {
            newSpell = Instantiate(spellProjectile[1], this.transform.position, spellProjectile[1].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WindWaveThrow>().spellNum = spellSelected;
            newSpell.GetComponent<WindWaveThrow>().maxRange = 30;
            newSpell.GetComponent<WindWaveThrow>().throwSpeed = 35;
            canCast[spellSelected] = false;
            dashDirection = spellSelected;
            dashAim = new Vector3(player1Aim.transform.position.x , player1Aim.transform.position.y, player1Aim.transform.position.z);
            dashDirectionTime = 75;
            if (this.transform.position.y < 2.5)
            {
                rb.AddForce(Vector3.up * 750);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }
            this.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Invulnrble Dash");

        }

    }
    private void WaterPull()
    {
        if (spellSecondary[spellSelected] == "")
        {
            newSpell = Instantiate(spellProjectile[2], this.transform.position, spellProjectile[0].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WaterPullThrow>().spellNum = spellSelected;
            //Debug.Log("Basic");
            newSpell.GetComponent<WaterPullThrow>().maxRange = 25;
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "AOE")
        {
            for (int i = 0; i < 5; i++)
            {
                newSpellAOE[i] = Instantiate(spellProjectile[2], this.transform.position, spellProjectile[0].transform.rotation);
                newSpellAOE[i].transform.position = new Vector3(newSpellAOE[i].transform.position.x, newSpellAOE[i].transform.position.y - .25f, newSpellAOE[i].transform.position.z);
                newSpellAOE[i].GetComponent<WaterPullThrow>().spellNum = spellSelected;
                newSpellAOE[i].GetComponent<WaterPullThrow>().maxRange = 20;
                // I got Really Really Fucking Lazy and Hard Coded the Draw Cricle about point function to make this work. 
                //Im ashamed of the following code and wil fix when i figrue out abetter draw circle - Mark
                if (i == 0)
                {
                    AOEpoint.position = player1Aim.transform.position;
                }
                if (spellSelected == 0 || spellSelected == 2)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x + 3, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x + 3, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 3)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x - 9, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 4)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x - 3, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                }
                if (spellSelected == 1 || spellSelected == 3)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z + 3);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z + 3);
                    }
                    if (i == 3)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z - 9);
                    }
                    if (i == 4)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z - 3);
                    }
                }

                newSpellAOE[i].GetComponent<WaterPullThrow>().transform.LookAt(AOEpoint);
            }
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "Range")
        {
            newSpell = Instantiate(spellProjectile[2], this.transform.position, spellProjectile[0].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WaterPullThrow>().spellNum = spellSelected;
            newSpell.GetComponent<WaterPullThrow>().maxRange = 75;
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "Dash")
        {
            newSpell = Instantiate(spellProjectile[2], this.transform.position, spellProjectile[0].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WaterPullThrow>().spellNum = spellSelected;
            newSpell.GetComponent<WaterPullThrow>().maxRange = 30;
            newSpell.GetComponent<WaterPullThrow>().throwSpeed = 35;
            canCast[spellSelected] = false;
            dashDirection = spellSelected;
            dashAim = new Vector3(player1Aim.transform.position.x, player1Aim.transform.position.y, player1Aim.transform.position.z);
            dashDirectionTime = 75;

            if (this.transform.position.y < 2.5)
            {
                rb.AddForce(Vector3.up * 750);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }
            this.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Invulnrble Dash");

        }



    }
}
       