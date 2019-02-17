using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTrailThrow : MonoBehaviour
{
    public GameObject cardTrailTarget;
    public float trailSpeed = 1f;
    public int life;

    void Update()
    {

        if (life > 1)
        {
            Destroy(this.gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, cardTrailTarget.transform.position, trailSpeed);
        life++;
    }
}
