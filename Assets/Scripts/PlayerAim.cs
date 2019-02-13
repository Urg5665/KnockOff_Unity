using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform parent;
    public GameObject player;

    public GameObject cardTarget;
    public List<GameObject> allTargets;

    public int spaceOff;
    public float heightOff;

    public bool cardThrown;



    // Update is called once per frame
    void Update()
    {


            if (Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(parent.position.x - spaceOff, parent.position.y - heightOff, parent.position.z);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(parent.position.x, parent.position.y - heightOff, parent.position.z + (spaceOff * 2));
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(parent.position.x + spaceOff, parent.position.y - heightOff, parent.position.z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = new Vector3(parent.position.x, parent.position.y - heightOff, parent.position.z - (spaceOff * 2));
            }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject targetSave = Instantiate(cardTarget);
            targetSave.transform.position = this.transform.position;
            allTargets.Add(targetSave);
        }


        

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
