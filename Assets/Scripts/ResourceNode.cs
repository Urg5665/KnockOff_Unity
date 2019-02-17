using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject[] resObjects;
    public GameObject currentSpawn; // child res

    public int resType;

    public int i ;

    private void Awake()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        resType = Mathf.RoundToInt(Random.Range(0, 2));
        currentSpawn = Instantiate(resObjects[resType], this.transform);
        i = 101;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawn == null)
        {
            resType = Mathf.RoundToInt(Random.Range(0, 2));
            currentSpawn = Instantiate(resObjects[resType], this.transform);
            currentSpawn.SetActive(false);
            i = 0;
        }
        if (i == 100)
        {
            currentSpawn.SetActive(true);
        }
        i++;
    }
}
