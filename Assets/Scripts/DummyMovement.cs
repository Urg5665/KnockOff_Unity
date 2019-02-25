using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    public int i;

    // Update is called once per frame
    void Update()
    {
        i++;

        if (i < 50 && this.transform.position.y < 3 && this.transform.position.x < 40)
        {
            this.transform.position = new Vector3(this.transform.position.x - .15f, this.transform.position.y, this.transform.position.z + .15f);
        }
        else if (i < 100 && this.transform.position.y < 3 && this.transform.position.x < 40)
        {
            this.transform.position = new Vector3(this.transform.position.x + .15f, this.transform.position.y, this.transform.position.z - .15f);
        }
        else if (i < 150 && this.transform.position.y < 3 && this.transform.position.x < 40)
        {
            this.transform.position = new Vector3(this.transform.position.x - .15f, this.transform.position.y, this.transform.position.z - .15f);
        }
        else if (i < 200 && this.transform.position.y < 3 && this.transform.position.x < 40)
        {
            this.transform.position = new Vector3(this.transform.position.x + .15f, this.transform.position.y, this.transform.position.z + .15f);
        }
        else
        {
            i = 0;
        }

    }
}
