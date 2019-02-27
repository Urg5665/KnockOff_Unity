using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimXbox : MonoBehaviour
{
    public Transform parent;
    public GameObject player;
    public PlayerControlXbox playerControlXbox;

    public float xDif;
    public float zDif;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 25;
    }

    // Update is called once per frame
    void Update()
    {
        xDif = Mathf.Abs(this.transform.position.x - parent.position.x);
        zDif = Mathf.Abs(this.transform.position.z - parent.position.z);

        if ( xDif > 50)
        {
            this.transform.position = new Vector3(parent.position.x, transform.position.y, parent.position.z);
        }

        if (zDif > 50)
        {
            this.transform.position = new Vector3(parent.position.x, transform.position.y, parent.position.z);
        }

        if (Input.GetAxis("HorAim") > 0)
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        if (Input.GetAxis("HorAim") < 0)
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        if (Input.GetAxis("VerAim") < 0)
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        if (Input.GetAxis("VerAim") > 0)
                transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        



    }
}
