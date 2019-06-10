using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRotate : MonoBehaviour
{
    public int playerBelong;
    public GameObject playerMesh;
    public GameObject playerAim;
    public GameObject player;
    void Update()
    {
        if (playerBelong == 1)
        {
            transform.LookAt(playerAim.transform.position);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 45, transform.eulerAngles.z);
            if (player.GetComponent<BoxCollider>().isTrigger)
            {
                //playerMesh.SetActive(false);
                //player.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                playerMesh.SetActive(true);
                player.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else if (playerBelong == 2)
        {
            transform.LookAt(playerAim.transform.position);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 45, transform.eulerAngles.z);
            if (player.GetComponent<BoxCollider>().isTrigger)
            {
                //playerMesh.SetActive(false);
                //player.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                playerMesh.SetActive(true);
                player.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
