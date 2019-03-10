using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShotUI_DashRotate : MonoBehaviour
{
    public GameObject playerBelong;
    public GameObject playerTarget;

    public PlayerControl playerControl;
    public PlayerControlXbox playerControlXbox;

    public GameObject baseSkillShot;

    public float xPos;
    public float zPos;
    public float zDif;
    public float xDif;
    public float hypo;
    public float angle;
    public float angle2;
    private void Start()
    {
        if (playerBelong.name == "Player1")
        {
            playerControl = playerBelong.GetComponent<PlayerControl>();
            playerTarget = GameObject.Find("Player2");
        }
        if (playerBelong.name == "Player2")
        {
            playerControlXbox = playerBelong.GetComponent<PlayerControlXbox>();
            playerTarget = GameObject.Find("Player1");
        }
    }


    // Update is called once per frame
    void Update()
    {
        //SkillShotUIUpdate(); // Visual Updates
        xPos = this.transform.position.x - playerTarget.transform.position.x;
        zPos = this.transform.position.z - playerTarget.transform.position.z;
        xDif = Mathf.Abs(this.transform.position.x - playerTarget.transform.position.x);
        zDif = Mathf.Abs(this.transform.position.z - playerTarget.transform.position.z);

        hypo = Mathf.Sqrt((xDif * xDif + zDif * zDif));

        angle = Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));

        if (angle > 45) // north south
        {
            if (zPos > 0) // South
            {
                if (xPos > 0)
                {
                    angle2 = 270 + Mathf.Rad2Deg * (Mathf.Asin(xDif / hypo));
                }
                if (xPos <= 0)
                {
                    angle2 = 270 - Mathf.Rad2Deg * (Mathf.Asin(xDif / hypo));
                }
            }
            if (zPos <= 0)
            {
                if (xPos > 0)
                {
                    angle2 = 90 - Mathf.Rad2Deg * (Mathf.Asin(xDif / hypo));
                }
                if (xPos <= 0)
                {
                    angle2 = 90 + Mathf.Rad2Deg * (Mathf.Asin(xDif / hypo));
                }
            }
        }
        else if (angle <= 45) // east west
        {
            if (xPos > 0)
            {

                if (zPos > 0)
                {
                    angle2 = 180 - Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));
                }
                if (zPos <= 0)
                {
                    angle2 = 180 + Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));
                }
            }
            if (xPos <= 0)
            {
                if (zPos > 0)
                {
                    angle2 = Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));
                }
                if (zPos <= 0)
                {
                    angle2 = 360 - Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));
                }

            }
        }

        transform.localEulerAngles = new Vector3(0, 0, angle2);
    }

}
