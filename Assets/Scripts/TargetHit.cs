using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour
{
    public int i = 0;

    private void Update()
    {
        i++;
        if (i > 200)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Card")
        {
            Destroy(this.gameObject);
        }
    }
}
