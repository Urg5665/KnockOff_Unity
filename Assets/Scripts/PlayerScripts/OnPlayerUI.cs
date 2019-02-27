using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnPlayerUI : MonoBehaviour
{
    public GameObject playerBelong;
    public PlayerControl playerControl;

    public GameObject[] spellUI;

    public Sprite aoeSprite;
    public Sprite rangeSprite;
    public Sprite dashSprite;


    public Sprite white;
    public Sprite red;
    public Sprite cyan;
    public Sprite blue;



    // Start is called before the first frame update
    void Start()
    {
        playerControl = playerBelong.GetComponent<PlayerControl>();


    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playerControl.spellSecondary[i] == "AOE")
            {
                spellUI[i].GetComponent<Image>().sprite = aoeSprite;
            }
            if (playerControl.spellSecondary[i] == "Range")
            {
                spellUI[i].GetComponent<Image>().sprite = rangeSprite;
            }
            if (playerControl.spellSecondary[i] == "Dash")
            {
                spellUI[i].GetComponent<Image>().sprite = dashSprite;
            }
            if (playerControl.spellSecondary[i] == "")
            {
                spellUI[i].GetComponent<Image>().sprite = null;
            }

        }
    }
}