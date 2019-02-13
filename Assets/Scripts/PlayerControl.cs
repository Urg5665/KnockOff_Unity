using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int playerNum;

    public List<GameObject> spells;
    public int spellSelected;
    public float speed;
    public GameObject card;
    public GameObject newCard;

    public int cardsThrown;


    public Rigidbody rb;

    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        cardsThrown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded)
        {
            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
            if (Input.GetKey(KeyCode.S))
                transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);


            if (Input.GetMouseButtonDown(0))
            {  
                newCard = Instantiate(card, this.transform.position, card.transform.rotation);
                newCard.transform.position = new Vector3(newCard.transform.position.x, newCard.transform.position.y - .25f, newCard.transform.position.z);
                cardsThrown++;
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
        }

    }




}
 

