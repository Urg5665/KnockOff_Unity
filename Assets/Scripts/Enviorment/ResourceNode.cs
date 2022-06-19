﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject[] resObjects;
    public GameObject currentSpawn; // child res

    public int resType;

    public int respawnCounter;
    public int respawnTime;

    public AudioSource audioSource;

    private void Awake()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        resType = Mathf.RoundToInt(Random.Range(0,2)); // 0 to 4 // 1  fire, 2 air, 3 water, 4 earth
        currentSpawn = Instantiate(resObjects[resType], this.transform);
        respawnCounter = 501;
        respawnTime = 1000;
        audioSource = this.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (currentSpawn == null)
        {
            audioSource.Play();
            resType = Mathf.RoundToInt(Random.Range(0, 2));
            currentSpawn = Instantiate(resObjects[resType], this.transform);
            currentSpawn.SetActive(false);
            respawnCounter = 0;
        }
        if (respawnCounter == respawnTime)
        {
            currentSpawn.SetActive(true);
        }
        respawnCounter++;
    }
}
