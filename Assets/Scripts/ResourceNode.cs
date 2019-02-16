using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public GameObject[] resObjects;
    public GameObject currentSpawn;

    public int i = 10;

    // Update is called once per frame
    void Update()
    {
        
        if (i == 100)
        {
            //currentSpawn = Instantiate(resObjects[Mathf.RoundToInt(Random.Range(0, 1))], this.transform);
        }
        i++;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            Destroy(currentSpawn.gameObject);
            i = 0;
            Debug.Log("HitByCard");
        }
    }
}
