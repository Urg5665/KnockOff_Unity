﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceChild : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player1")
        {
            Destroy(this.gameObject);
        }
    }
}
