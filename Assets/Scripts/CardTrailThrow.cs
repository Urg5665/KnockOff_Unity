using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTrailThrow : MonoBehaviour
{
    public GameObject cardTrailTarget;
    public float trailSpeed = 1f;

    void Update()
    {


        transform.position = Vector3.MoveTowards(transform.position, cardTrailTarget.transform.position, trailSpeed);
    }
}
