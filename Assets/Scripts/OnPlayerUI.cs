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
            if (playerControl.spellPrimary[i] == "Fire")
            {
                spellUI[i].GetComponent<OnPlayerUISelect>().state.highlightedSprite = red;
            }
            if (playerControl.spellPrimary[i] == "Wind")
            {
                spellUI[i].GetComponent<OnPlayerUISelect>().state.highlightedSprite = cyan;
            }
            if (playerControl.spellPrimary[i] == "Water")
            {
                spellUI[i].GetComponent<OnPlayerUISelect>().state.highlightedSprite = blue;
            }
            if (playerControl.spellPrimary[i] == "")
            {
                spellUI[i].GetComponent<OnPlayerUISelect>().state.highlightedSprite = white;
            }
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