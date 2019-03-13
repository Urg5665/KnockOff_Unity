using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRotate : MonoBehaviour
{
    public GameObject playerAim;

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(playerAim.transform.position);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 45, transform.eulerAngles.z);

    }
}
