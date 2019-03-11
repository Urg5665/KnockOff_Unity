using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTrailThrow : MonoBehaviour
{
    public GameObject cardTrailTarget;
    public float trailSpeed = 1f;
    public int life;

    public void Start()
    {
        life = 0;
    }

    void Update()
    {
        if (life > 500)
        {
            if (cardTrailTarget != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, cardTrailTarget.transform.position, trailSpeed);
                life++;
            }
        }
        if (life == 500 || cardTrailTarget == null)
        {
            Destroy(this.gameObject);
        }

    }
}
