using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int playerNum;

    public List<GameObject> spells;
    public int spellSelected;
    public bool[] canCast;
    public float speed;
    public GameObject card;
    public GameObject newCard;
    public int cardsThrown;
    public Transform movement;

    public Rigidbody rb;

    public bool grounded;

    void Start()
    {
        grounded = true;
        cardsThrown = 0;
        canCast = new bool[4]; // ignore zero here
        for(int i = 0; i < 4; i++)
        {
            canCast[i] = true;
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
            float moveHL = Input.GetAxis("Horizontal") * speed;
            float moveVL = Input.GetAxis("Vertical") * speed;
            /*
            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.S))
                transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
                */
            moveHL *= Time.deltaTime;
            moveVL *= Time.deltaTime;

            movement.Translate(moveVL,0, moveHL);

          

            if (Input.GetMouseButtonDown(0) && cardsThrown < 4 && canCast[spellSelected]) // fire Card
            {  
                newCard = Instantiate(card, this.transform.position, card.transform.rotation);
                newCard.transform.position = new Vector3(newCard.transform.position.x, newCard.transform.position.y - .25f, newCard.transform.position.z);
                newCard.GetComponent<CardThrow>().cardNum = spellSelected;
                Debug.Log("Card" + (spellSelected + 1) + " Thrown");
                cardsThrown++;
                speed = speed - 1.5f; // slow aplied for each card in play
                canCast[spellSelected] = false;
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
            speed = speed + 1.5f; // slow aplied for each card in play
            canCast[collision.GetComponent<CardThrow>().cardNum] = true;
        }

    }




}
 

