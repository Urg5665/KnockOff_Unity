using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    

    public GameObject[] resObjects;
    public GameObject currentSpawn;

    public int resIndex;

    public int i = 100;

    private void Awake()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        resIndex = Mathf.RoundToInt(Random.Range(0, 2));
        currentSpawn = Instantiate(resObjects[resIndex], this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawn == null)
        {
            resIndex = Mathf.RoundToInt(Random.Range(0, 2));
            i = 0;
        }

        if (i == 100)
        {
            currentSpawn = Instantiate(resObjects[resIndex], this.transform);
        }
        i++;
    }
}
