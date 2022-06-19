using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    public int i;

    public bool fireBeserk = false;

    // Update is called once per frame
    void Update()
    {
        /*
        i++;

        if (i < 50 && this.transform.position.y < 3.5 && this.transform.position.x < 40)
        {
            this.transform.position = new Vector3(this.transform.position.x - .10f, this.transform.position.y, this.transform.position.z + .10f);
        }
        else if (i < 100 && this.transform.position.y < 3.5 && this.transform.position.x < 40)
        {
            this.transform.position = new Vector3(this.transform.position.x + .10f, this.transform.position.y, this.transform.position.z - .10f);
        }
        */
        /*else if (i < 150 && this.transform.position.y < 3 && this.transform.position.x < 40)
        {
            this.transform.position = new Vector3(this.transform.position.x - .15f, this.transform.position.y, this.transform.position.z - .15f);
        }
        else if (i < 200 && this.transform.position.y < 3 && this.transform.position.x < 40)
        {
            this.transform.position = new Vector3(this.transform.position.x + .15f, this.transform.position.y, this.transform.position.z + .15f);
        }*/
        /*
        else
        {
            i = 0;
        }
        */
        if (fireBeserk)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 300 * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 3.0f , transform.position.z);
            print("In Fire beserk DummyMove");
        }


    }

    public void FireBeserk()
    {
        fireBeserk = true;
    }
}
