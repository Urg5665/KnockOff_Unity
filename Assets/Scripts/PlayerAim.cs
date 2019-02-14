using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform parent;
    public GameObject player;

    public int spaceOff;
    public float heightOff;

    public int i = 0;

    void Update()
    {
        if (i > 15)
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(parent.position.x - spaceOff, parent.position.y, parent.position.z);
                i = 0;
            }
            // Left Down
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(parent.position.x - spaceOff / 2, parent.position.y - heightOff, parent.position.z - spaceOff / 2);
                i = 0;
            }
            // Left Up
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(parent.position.x - spaceOff / 2, parent.position.y - heightOff, parent.position.z + spaceOff / 2);
                i = 0;
            }
            // Right
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(parent.position.x + spaceOff, parent.position.y, parent.position.z);
                i = 0;
            }
            // Right Down
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(parent.position.x + spaceOff / 2, parent.position.y - heightOff, parent.position.z - spaceOff / 2);
                i = 0;
            }
            // Rigth Up
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(parent.position.x + spaceOff / 2, parent.position.y - heightOff, parent.position.z + spaceOff / 2);
                i = 0;
            }

            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(parent.position.x, parent.position.y, parent.position.z + (spaceOff * 2));
                i = 0;
            }

            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(parent.position.x, parent.position.y, parent.position.z - (spaceOff * 2));
                i = 0;
            }
        }
        i++;
            // Left
           


    }
}
/*        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(parent.position.x - spaceOff/2, parent.position.y - heightOff, parent.position.z + spaceOff );
        }

         if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(parent.position.x + spaceOff / 2, parent.position.y - heightOff, parent.position.z + spaceOff);
        }


         if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(parent.position.x + spaceOff / 2, parent.position.y - heightOff, parent.position.z - spaceOff);
        }


         if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(parent.position.x - spaceOff / 2, parent.position.y - heightOff, parent.position.z - spaceOff);
        }
 */
