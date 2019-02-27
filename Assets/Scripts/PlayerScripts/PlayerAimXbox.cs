using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimXbox : MonoBehaviour
{
    public Transform parent;
    public GameObject player;
    public PlayerControlXbox playerControlXbox;

    public float xDif;
    public float zDif;

    public float xPos;
    public float zPos;

    public float hypo;

    public float angle;

    public float speed;

    public int spellSelected;
    // Start is called before the first frame update
    void Start()
    {
        speed = 25;
    }

    // Update is called once per frame
    void Update()
    {
        xPos = this.transform.position.x - parent.position.x;
        zPos = this.transform.position.z - parent.position.z;
        xDif = Mathf.Abs(this.transform.position.x - parent.position.x);
        zDif = Mathf.Abs(this.transform.position.z - parent.position.z);

        hypo = Mathf.Sqrt((xDif * xDif + zDif * zDif));

        angle = Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));

        //Debug.Log(angle);

        if ( angle > 45) // north south
        {
            if(zPos > 0)
            {
                //Debug.Log("P2 North");
                spellSelected = 0;
            }
            if (zPos <= 0)
            {
                //Debug.Log("P2 South");
                spellSelected = 2;
            }
        }
        else if ( angle <= 45) // east west
        {
            if (xPos > 0)
            {
                //Debug.Log("P2 East");
                spellSelected = 1;
            }
            if (xPos <= 0)
            {
                //Debug.Log("P2 West");
                spellSelected = 3;
            }
        }







        if ( xDif > 50)
        {
            this.transform.position = new Vector3(parent.position.x, transform.position.y, parent.position.z);
        }

        if (zDif > 50)
        {
            this.transform.position = new Vector3(parent.position.x, transform.position.y, parent.position.z);
        }

        if (Input.GetAxis("HorAim") > 0)
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        if (Input.GetAxis("HorAim") < 0)
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        if (Input.GetAxis("VerAim") < 0)
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        if (Input.GetAxis("VerAim") > 0)
                transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        



    }
}
