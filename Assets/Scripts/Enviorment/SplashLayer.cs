using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLayer : MonoBehaviour
{

    public GameObject splash;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Cliffs")
        {
            GameObject splashClone = Instantiate(splash);
            Transform splashLocation = collision.transform;
            splashClone.transform.position = new Vector3(splashLocation.transform.position.x, splashLocation.transform.position.y, splashLocation.transform.position.z);
            splashClone = null;
        }

    }
}
