using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShotRotate : MonoBehaviour
{
    public GameObject playerAim;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerAim.transform);   
    }
}
