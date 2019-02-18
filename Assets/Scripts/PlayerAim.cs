using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform parent;
    public GameObject player;
    public PlayerControl playerControl;

    public int spaceOff;
    public float heightOff;

  




    void Update()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(parent.position.x - spaceOff, parent.position.y, parent.position.z);
        }
        // Left Down
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(parent.position.x - spaceOff / 2, parent.position.y, parent.position.z - spaceOff / 2);
        }
        // Left Up
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(parent.position.x - spaceOff / 2, parent.position.y, parent.position.z + spaceOff / 2);
        }
        // Right
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(parent.position.x + spaceOff, parent.position.y, parent.position.z);
        }
        // Right Down
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(parent.position.x + spaceOff / 2, parent.position.y, parent.position.z - spaceOff / 2);
        }
        // Rigth Up
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(parent.position.x + spaceOff / 2, parent.position.y, parent.position.z + spaceOff / 2);
        }
        else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(parent.position.x, parent.position.y, parent.position.z + (spaceOff * 2));
        }

        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(parent.position.x, parent.position.y, parent.position.z - (spaceOff * 2));
        }


    }
}
/* 
 * 
 *         if (playerControl.spellSelected == 0)
        {
            transform.position = new Vector3(parent.position.x, parent.position.y, parent.position.z + (spaceOff * 2));
        }
        if (playerControl.spellSelected == 1)
        {
            transform.position = new Vector3(parent.position.x + spaceOff, parent.position.y, parent.position.z);
        }
        if (playerControl.spellSelected == 2)
        {
            transform.position = new Vector3(parent.position.x, parent.position.y, parent.position.z - (spaceOff * 2));
        }
        if (playerControl.spellSelected == 3)
        {
            transform.position = new Vector3(parent.position.x - spaceOff, parent.position.y, parent.position.z);
        }

 * 
 * 
 */
