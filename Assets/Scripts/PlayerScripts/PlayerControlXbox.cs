using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlXbox : MonoBehaviour
{
    public GameObject player2Aim;
    //public PlayerAim playerAim;

    public int playerNum;
    public float speed;
    public GameObject playerUI;
    //public float maxSpeed = 10;

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

    public int dashDirection; // This is to check if you are fireing a particle afterwards, if still facing the same direction
    public bool dashing;
    public int dashingTime;
    public int dashDirectionTime;
    public Vector3 dashAim;
    public float waterDashForceUp;
    public bool castAfterDash;
    public int dashLength;
    public int cardsThrown;
    public float slowDownPerCard = 2.5f;

    public GameObject newSpell;
    public GameObject[] newSpellAOE;

    public int baseRange;
    public int baseSpeed;

    public int aoeRange;
    public float aoeWidth;

    public int rangeRange;
    public int rangeSpeed;

    public int dashSpellRange;

    // Testing Stun out on Player
    public int stunLength;
    public Text onPlayerText;

    public bool airBorn;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        grounded = true;
        touchingWall = false;
        cardsThrown = 0;
        canCast = new bool[4]; // ignore zero here
        //onPlayerUIButton = new GameObject[4];
        waterDashForceUp = 0;
        dashDirectionTime = 0;
        dashing = false;
        dashingTime = 0;
        castAfterDash = false;
        dashLength = 20;
        spellSelected = 0;

        baseRange = 30;
        baseSpeed = 40;
        aoeRange = 30;

        rangeRange = 90;
        rangeSpeed = 70;

        dashSpellRange = 20; // should be very close

        stunLength = 0;

        for (int i = 0; i < 4; i++)
        {
            canCast[i] = true;
            spellPrimary[i] = "";
            spellSecondary[i] = "";
        }
        slowDownPerCard = 2.5f;
        player2Aim = GameObject.Find("Player2Aim");
    }

    public void pickDirection()
    {
        spellSelected = player2Aim.GetComponent<PlayerAimXbox>().spellSelected;
    }
    
    void Update()
    {
        //Debug.Log("CheckUpdate");

        pickDirection();
        dashDirectionTime--;
        aoeWidth = (Vector3.Distance(player2Aim.transform.position, transform.position)) / 2;

        //speed = maxSpeed - (slowDownPerCard * cardsThrown); // apply slow for each card in play
        //Debug.Log("speed" + speed);

        if (stunLength > 0)
        {
            //Debug.Log("Player2 Stunned");
            stunLength--;
            onPlayerText.text = "STUNED";
        }
        if (stunLength == 0)
        {
            speed = 7.5f;
            onPlayerText.text = "";
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.left * 600);
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 400);
        }


        if (dashing)
        {
            for (int i = 0; i < 4; i++)
            {
                canCast[i] = false;
            }
            speed = 7.5f;
            onPlayerText.text = "";
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerUI.SetActive(false);
            dashingTime++;
            transform.Translate(Vector3.forward * Time.deltaTime * speed * 5, Space.Self);
            if (this.transform.position.y < 2.5)
            {
                this.GetComponent<BoxCollider>().enabled = true; // can fail recover
                Vector3 above = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, above, Time.deltaTime);
                //transform.Translate(Vector3.up * Time.deltaTime * speed * 5, Space.Self);
            }
            else
            {
                this.GetComponent<BoxCollider>().enabled = false;
            }
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        if (dashing && dashingTime > dashLength)
        {
            dashingTime = 0;
            dashing = false;
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * 3, Space.Self);
            this.GetComponent<BoxCollider>().enabled = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            this.transform.rotation = Quaternion.Euler(0, 45, 0);
            playerUI.SetActive(true);
            castAfterDash = true;
        }
        if (castAfterDash)
        {
            castAfterDash = false;
            for (int i = 0; i < 4; i++)
            {
                canCast[i] = true;
            }
            if (spellPrimary[dashDirection] == "Fire")
            {
                newSpell = Instantiate(spellProjectile[0], this.transform.position, spellProjectile[0].transform.rotation);
                newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
                newSpell.GetComponent<FireBallThrow>().spellNum = dashDirection;
                newSpell.GetComponent<FireBallThrow>().maxRange = dashSpellRange;
                newSpell.GetComponent<FireBallThrow>().dashSpell = true;
            }
            if (spellPrimary[dashDirection] == "Wind")
            {
                newSpell = Instantiate(spellProjectile[1], this.transform.position, spellProjectile[0].transform.rotation);
                newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
                newSpell.GetComponent<WindWaveThrow>().spellNum = dashDirection;
                newSpell.GetComponent<WindWaveThrow>().maxRange = dashSpellRange;
                newSpell.GetComponent<WindWaveThrow>().dashSpell = true;
            }
            if (spellPrimary[dashDirection] == "Water")
            {
                newSpell = Instantiate(spellProjectile[2], this.transform.position, spellProjectile[0].transform.rotation);
                newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
                newSpell.GetComponent<WaterPullThrow>().spellNum = dashDirection;
                newSpell.GetComponent<WaterPullThrow>().maxRange = dashSpellRange;
                newSpell.GetComponent<WaterPullThrow>().dashSpell = true;
            }
            spellPrimary[dashDirection] = "";
            spellSecondary[dashDirection] = "";
            canCast[dashDirection] = true;
        }
            // Card Casting Commands
            if (Input.GetAxis("CardThrow") == 1 && cardsThrown < 4 && canCast[spellSelected] && spellSecondary[spellSelected] == "") // Shoot Card
        {
            CardGather();
        }
        if (Input.GetAxis("CardThrow") == 1 && cardsThrown < 4 && canCast[spellSelected] && spellSecondary[spellSelected] != "")  // Disabel Shooitng Card because spell is maxed
        {
            //Debug.Log("Spell Maxed - Cast it!");
        }


        // Spell Casting Commands
        if (Input.GetAxis("SpellThrow") == 1 && cardsThrown < 4 && canCast[spellSelected] && spellPrimary[spellSelected] == "") // You Have no Spell
        {
            //Debug.Log("No Spell Avaliable");
        }
        if (Input.GetAxis("SpellThrow") == 1 && cardsThrown < 4 && canCast[spellSelected] && spellPrimary[spellSelected] == "Fire") // Shoot Fireball
        {
            Fireball();
        }
        if (Input.GetAxis("SpellThrow") == 1 && cardsThrown < 4 && canCast[spellSelected] && spellPrimary[spellSelected] == "Wind") // Shoot Wind Knock
        {
            WindKnockback();
        }
        if (Input.GetAxis("SpellThrow") == 1 && cardsThrown < 4 && canCast[spellSelected] && spellPrimary[spellSelected] == "Water") // Shoot Wind Knock
        {
            WaterPull();
        }

        Debug.Log(airBorn);

        if (grounded) // movement
        {
            if (Input.GetAxis("Horizontal") > 0)
                transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
            if (Input.GetAxis("Horizontal") < 0)
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
            if (Input.GetAxis("Vertical") < 0)
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            if (Input.GetAxis("Vertical") > 0)
                transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);


        }
        if (this.transform.position.y < 2.5f || this.transform.position.y > 3f)
        {
            grounded = false;
            airBorn = true;
        }
        if (this.transform.position.y >= 2.5f && this.transform.position.y <= 3f)
        {
            grounded = true;
            if (airBorn)
            {
                this.GetComponent<BoxCollider>().enabled = true;
                airBorn = false;
            }

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
            newSpell.GetComponent<FireBallThrow>().maxRange = baseRange;
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "AOE")
        {
            for (int i = 0; i < 5; i++)
            {
                newSpellAOE[i] = Instantiate(spellProjectile[0], this.transform.position, spellProjectile[0].transform.rotation);
                newSpellAOE[i].transform.position = new Vector3(newSpellAOE[i].transform.position.x, newSpellAOE[i].transform.position.y - .25f, newSpellAOE[i].transform.position.z);
                newSpellAOE[i].GetComponent<FireBallThrow>().spellNum = spellSelected;
                newSpellAOE[i].GetComponent<FireBallThrow>().maxRange = aoeRange;
                // I got Really Really Fucking Lazy and Hard Coded the Draw Cricle about point function to make this work. 
                //Im ashamed of the following code and wil fix when i figrue out abetter draw circle - Mark
                if (i == 0)
                {
                    AOEpoint.position = player2Aim.transform.position;
                }
                if (spellSelected == 0 || spellSelected == 2)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x + aoeWidth, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x - (aoeWidth* 2), this.transform.position.y, AOEpoint.transform.position.z);
                    }
                }
                if (spellSelected == 1 || spellSelected == 3)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z + aoeWidth);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z - (aoeWidth* 2));
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
            newSpell.GetComponent<FireBallThrow>().maxRange = rangeRange;
            newSpell.GetComponent<FireBallThrow>().throwSpeed = rangeSpeed;
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "Dash")
        {
            //Debug.Log("Dash");
            canCast[spellSelected] = false;
            dashing = true;
            dashDirection = spellSelected;
            dashAim = new Vector3(player2Aim.transform.position.x, player2Aim.transform.position.y, player2Aim.transform.position.z);
            dashDirectionTime = 75;
            transform.LookAt(dashAim);
            if (this.transform.position.y < 2.5)
            {
                rb.AddForce(Vector3.up * waterDashForceUp);
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
            newSpell.GetComponent<WindWaveThrow>().maxRange = baseRange;
        }
        else if (spellSecondary[spellSelected] == "AOE")
        {
            for (int i = 0; i < 8; i++)
            {
                newSpellAOE[i] = Instantiate(spellProjectile[1], this.transform.position, spellProjectile[0].transform.rotation);
                newSpellAOE[i].transform.position = new Vector3(newSpellAOE[i].transform.position.x, newSpellAOE[i].transform.position.y - .25f, newSpellAOE[i].transform.position.z);
                newSpellAOE[i].GetComponent<WindWaveThrow>().spellNum = spellSelected;
                newSpellAOE[i].GetComponent<WindWaveThrow>().maxRange = aoeRange;
                // I got Really Really Fucking Lazy and Hard Coded the Draw Cricle about point function to make this work. 
                //Im ashamed of the following code and wil fix when i figrue out abetter draw circle - Mark
                if (i == 0)
                {
                    AOEpoint.position = player2Aim.transform.position;
                }
                if (spellSelected == 0 || spellSelected == 2)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x + aoeWidth, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x - (aoeWidth* 2), this.transform.position.y, AOEpoint.transform.position.z);
                    }
                }
                if (spellSelected == 1 || spellSelected == 3)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z + aoeWidth);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z - (aoeWidth* 2));
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
            newSpell.GetComponent<WindWaveThrow>().maxRange = rangeRange;
            newSpell.GetComponent<WindWaveThrow>().throwSpeed = rangeSpeed;
            canCast[spellSelected] = false;
        }
        else if (spellSecondary[spellSelected] == "Dash")
        {
            //Debug.Log("Dash");
            canCast[spellSelected] = false;
            dashing = true;
            dashDirection = spellSelected;
            dashAim = new Vector3(player2Aim.transform.position.x, player2Aim.transform.position.y, player2Aim.transform.position.z);
            dashDirectionTime = 75;
            transform.LookAt(dashAim);
            if (this.transform.position.y < 2.5)
            {
                rb.AddForce(Vector3.up * waterDashForceUp);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }

            this.GetComponent<BoxCollider>().enabled = false;
            //Debug.Log("Invulnrble Dash");

        }

    }
    private void WaterPull()
    {
        if (spellSecondary[spellSelected] == "")
        {
            newSpell = Instantiate(spellProjectile[2], this.transform.position, spellProjectile[2].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WaterPullThrow>().spellNum = spellSelected;
            //Debug.Log("Basic");
            newSpell.GetComponent<WaterPullThrow>().maxRange = baseRange;
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "AOE")
        {
            for (int i = 0; i < 5; i++)
            {
                newSpellAOE[i] = Instantiate(spellProjectile[2], this.transform.position, spellProjectile[2].transform.rotation);
                newSpellAOE[i].transform.position = new Vector3(newSpellAOE[i].transform.position.x, newSpellAOE[i].transform.position.y - .25f, newSpellAOE[i].transform.position.z);
                newSpellAOE[i].GetComponent<WaterPullThrow>().spellNum = spellSelected;
                newSpellAOE[i].GetComponent<WaterPullThrow>().maxRange = aoeRange;
                // I got Really Really Fucking Lazy and Hard Coded the Draw Cricle about point function to make this work. 
                //Im ashamed of the following code and wil fix when i figrue out abetter draw circle - Mark
                if (i == 0)
                {
                    AOEpoint.position = player2Aim.transform.position;
                }
                if (spellSelected == 0 || spellSelected == 2)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x + aoeWidth, this.transform.position.y, AOEpoint.transform.position.z);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x - (aoeWidth* 2), this.transform.position.y, AOEpoint.transform.position.z);
                    }
                }
                if (spellSelected == 1 || spellSelected == 3)
                {
                    if (i == 1)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z + aoeWidth);
                    }
                    if (i == 2)
                    {
                        AOEpoint.position = new Vector3(AOEpoint.transform.position.x, this.transform.position.y, AOEpoint.transform.position.z - (aoeWidth* 2));
                    }

                }

                newSpellAOE[i].GetComponent<WaterPullThrow>().transform.LookAt(AOEpoint);
            }
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "Range")
        {
            newSpell = Instantiate(spellProjectile[2], this.transform.position, spellProjectile[2].transform.rotation);
            newSpell.transform.position = new Vector3(newSpell.transform.position.x, newSpell.transform.position.y - .25f, newSpell.transform.position.z);
            newSpell.GetComponent<WaterPullThrow>().spellNum = spellSelected;
            newSpell.GetComponent<WaterPullThrow>().maxRange = rangeRange;
            newSpell.GetComponent<WaterPullThrow>().throwSpeed = rangeSpeed;
            canCast[spellSelected] = false;
        }
        if (spellSecondary[spellSelected] == "Dash")
        {
            //Debug.Log("Dash");
            canCast[spellSelected] = false;
            dashing = true;
            dashDirection = spellSelected;
            dashAim = new Vector3(player2Aim.transform.position.x, player2Aim.transform.position.y, player2Aim.transform.position.z);
            dashDirectionTime = 75;
            transform.LookAt(dashAim);
            if (this.transform.position.y < 2.5)
            {
                rb.AddForce(Vector3.up * waterDashForceUp);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }

            this.GetComponent<BoxCollider>().enabled = false;
            //Debug.Log("Invulnrble Dash");

        }



    }
}
