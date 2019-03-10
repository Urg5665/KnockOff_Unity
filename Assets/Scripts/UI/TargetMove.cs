using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public GameObject playerAim;

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        this.transform.position = new Vector3(playerAim.transform.position.x, this.transform.position.y, playerAim.transform.position.z);
    }
}
