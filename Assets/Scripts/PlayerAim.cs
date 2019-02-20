using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform parent;
    public GameObject player;
    public PlayerControl playerControl;

    public Camera cam;

    public LayerMask groundLayer;

    public Rigidbody rb;

    public void Update()
    {

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            this.transform.position = hit.point;
            
        }

        this.transform.position = new Vector3(this.transform.position.x, parent.transform.position.y, this.transform.position.z);


        Debug.Log(Vector2.Angle(parent.transform.position, this.transform.position));

    }

}



    


/*     v
 *     
 *         private Vector3 GetPointUnderCursor()
    {
        Vector2 screenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(screenPosition);

        RaycastHit hitPosition;

        Physics.Raycast(mouseWorldPosition, cam.transform.forward, out hitPosition, 10, groundLayer);

        transform.position = hitPosition.point;

        return hitPosition.point;

 *     
 *     
 *     
 *     oid Update()
    {
        screenPosition = Input.mousePosition;


        Vector3 relativePos = (player.transform.position  - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
        transform.Translate(0, 0, rateOfChange * Time.deltaTime);




    }
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
 *  if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
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

    
    private Vector3 GetPointUnderCursor()
    {
        Vector2 screenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(screenPosition);

        RaycastHit hitPosition;

        Physics.Raycast(mouseWorldPosition, cam.transform.forward, out hitPosition, 10, groundLayer);

        transform.position = hitPosition.point;

        return hitPosition.point;

    }
 */
