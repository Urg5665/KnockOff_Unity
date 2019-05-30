using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EarthParticleSystem : MonoBehaviour
{
    public int partLife;

    private void Awake()
    {
        this.transform.localScale = new Vector3(Random.Range(0.1f, .8f), Random.Range(0.1f, .5f), Random.Range(0.1f, .8f));
        this.transform.eulerAngles = new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        partLife++;
        this.transform.eulerAngles += new Vector3(2, 2, 2);
        if (partLife == 30)
        {
            Destroy(this.gameObject);
        }
        if (partLife < 10)
        {
            this.transform.localScale += new Vector3(.05f, .05f, .05f);
        }
        if (partLife > 20)
        {
            this.transform.localScale -= new Vector3(.05f, .05f, .05f);
        }
        
    }
}
