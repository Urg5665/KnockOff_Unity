using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRotate : MonoBehaviour
{
    public int playerBelong;
    public GameObject playerAim;

    void Update()
    {
        if (playerBelong == 1)
        {
            transform.LookAt(playerAim.transform.position);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 45, transform.eulerAngles.z);
        }
        else if (playerBelong == 2)
        {
            transform.LookAt(playerAim.transform.position);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 45, transform.eulerAngles.z);
        }


    }
}
