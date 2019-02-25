using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    int i;

    // Update is called once per frame
    void Update()
    {
        i++;

        if (i < 100 && this.transform.position.y < 3 && this.transform.position.x < 30)
        {
            this.transform.position = new Vector3(this.transform.position.x - .075f, this.transform.position.y, this.transform.position.z + .075f);
        }
        else if (i < 200 && this.transform.position.y < 3 && this.transform.position.x < 30)
        {
            this.transform.position = new Vector3(this.transform.position.x + .075f, this.transform.position.y, this.transform.position.z - .075f);
        }
        else
        {
            i = 0;
        }

    }
}
