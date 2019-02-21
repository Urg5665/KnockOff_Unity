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
        if (life > 500)
        {
            Destroy(this.gameObject);
        }
        if (cardTrailTarget.gameObject != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, cardTrailTarget.transform.position, trailSpeed);
            life++;
        }

    }
}
