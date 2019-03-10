using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillShotRotate : MonoBehaviour
{
    public GameObject playerAim;
    public float zRotate;

    public Sprite cardSkillShot;

    public Sprite aoeSkillShot;
    public Sprite rangeSkillShot;
    public Sprite dashSkillShot;

    public float xPos;
    public float zPos;
    public float zDif;
    public float xDif;
    public float hypo;
    public float angle;
    public float angle2;

    private void Start()
    {
        zRotate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        xPos = this.transform.position.x - playerAim.transform.position.x;
        zPos = this.transform.position.z - playerAim.transform.position.z;
        xDif = Mathf.Abs(this.transform.position.x - playerAim.transform.position.x);
        zDif = Mathf.Abs(this.transform.position.z - playerAim.transform.position.z);

        hypo = Mathf.Sqrt((xDif * xDif + zDif * zDif));

        angle = Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));

        //angle2 = Vector3.Angle(this.transform.position, playerAim.transform.position);
        //Quaternion rotation = Quaternion.LookRotation(playerAim.transform.position, Vector3.forward);
        //transform.rotation = rotation;
        //this.transform.position = new Vector3(playerAim.transform.position.x, transform.position.y, playerAim.transform.position.z);

        //transform.LookAt(playerAim.transform.position);
        zRotate += 1f;

        Debug.Log(angle);

        
        if (angle > 45) // north south
        {
            if (zPos > 0) // South
            {
                //Debug.Log("South");
                if (xPos > 0)
                {
                    angle2 = 270 - Mathf.Rad2Deg * (Mathf.Asin(xDif / hypo));
                }
                if (xPos <= 0)
                {
                    angle2 = 270 + Mathf.Rad2Deg * (Mathf.Asin(xDif / hypo));
                }
            }
            if (zPos <= 0) 
            {
               // Debug.Log("Nouth");
                if (xPos > 0)
                {
                    angle2 = 90 + Mathf.Rad2Deg * (Mathf.Asin(xDif / hypo));
                }
                if (xPos <= 0)
                {
                    angle2 = 90 - Mathf.Rad2Deg * (Mathf.Asin(xDif / hypo));
                }
            }
        }
        else if (angle <= 45) // east west
        {
            if (xPos > 0)
            {
                if (zPos > 0)
                {
                    angle2 = 180 + Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));
                }
                if (zPos <= 0)
                {
                    angle2 = 180 - Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));
                }
            }
            if (xPos <= 0)
            {
                //Debug.Log("P1 West");
                //spellSelected = 3;
                if (zPos > 0)
                {
                    angle2 = 360 - Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));
                }
                if (zPos <= 0)
                {
                    angle2 = Mathf.Rad2Deg * (Mathf.Asin(zDif / hypo));
                }

            }
        }

        //Debug.Log(angle2);

        transform.localEulerAngles = new Vector3(0, 0, angle2);
        //transform.EulerAngles = new Vector3(0, 0, zRotate);
        //transform.Rotate(0, 0, zRotate);
        //transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
    }
}

