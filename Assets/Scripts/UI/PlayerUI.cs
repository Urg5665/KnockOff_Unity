using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject playerBelong;
    public PlayerControl playerControl;
    public PlayerControlXbox playerControlXbox;

    public GameObject[] spellUI;
    public GameObject[] spellUISec;

    public Color32[] spellUIColor;
    public Color32[] spellUIColorSec;

    public Sprite fireSprite;
    public Sprite windSprite;
    public Sprite waterSprite;

    public Sprite emptySprite;

    public Sprite aoeSprite;
    public Sprite rangeSprite;
    public Sprite dashSprite;
    public Sprite dashSpriteReverse;

    private void Start()
    {
        if (playerBelong.name == "Player1")
        {
            playerControl = playerBelong.GetComponent<PlayerControl>();
        }
        if (playerBelong.name == "Player2")
        {
            playerControlXbox = playerBelong.GetComponent<PlayerControlXbox>();
        }
         spellUIColor = new Color32[4];
         spellUIColorSec = new Color32[4];

}

    void Update()
    {
        if (playerBelong.name == "Player1")
        {
            for (int i = 0; i < 4; i++)
            {
                spellUIColor[i] = spellUI[i].GetComponent<Image>().color;
                spellUIColorSec[i] = spellUISec[i].GetComponentInChildren<Image>().color;

                
                if (playerControl.spellPrimary[i] == "Fire")
                {
                    spellUI[i].GetComponent<Image>().sprite = fireSprite;

                }
                if (playerControl.spellPrimary[i] == "Wind")
                {
                    spellUI[i].GetComponent<Image>().sprite = windSprite;
                }
                if (playerControl.spellPrimary[i] == "Water")
                {
                    spellUI[i].GetComponent<Image>().sprite = waterSprite;
                }
                if (playerControl.spellPrimary[i] == "")
                {
                }
                if (playerControl.spellSecondary[i] == "AOE")
                {
                    spellUISec[i].GetComponentInChildren<Image>().sprite = aoeSprite;
                    //spellUISec[i].GetComponent<Image>().color = new Color32(255, 255, 255, 200);
                }
                if (playerControl.spellSecondary[i] == "Range")
                {
                    spellUISec[i].GetComponentInChildren<Image>().sprite = rangeSprite;
                    //spellUI[i].GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 200);
                }
                if (playerControl.spellSecondary[i] == "Dash")
                {
                    if (i == 3)
                    {
                        spellUISec[i].GetComponentInChildren<Image>().sprite = dashSpriteReverse;
                        //spellUISec[i].GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 200);
                    }
                    else
                    {
                        spellUISec[i].GetComponentInChildren<Image>().sprite = dashSprite;
                        //spellUISec[i].GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 200);
                    }

                }
                if (playerControl.spellPrimary[i] == "")
                {
                    spellUI[i].GetComponent<Image>().sprite = emptySprite;
                    spellUISec[i].GetComponent<Image>().sprite = emptySprite;
                }
                if (i == playerControl.spellSelected)
                {
                    spellUIColor[i].a = 255;
                    spellUIColorSec[i].a = 255;
                    spellUI[i].GetComponent<Image>().color = spellUIColor[i];
                    spellUISec[i].GetComponentInChildren<Image>().color = spellUIColorSec[i];

                }
                if (i != playerControl.spellSelected)
                {
                    spellUIColor[i].a = 60;
                    spellUIColorSec[i].a = 60;
                    spellUI[i].GetComponent<Image>().color = spellUIColor[i];
                    spellUISec[i].GetComponentInChildren<Image>().color = spellUIColorSec[i];
                }

            }
        }
        if (playerBelong.name == "Player2")
        {
            for (int i = 0; i < 4; i++)
            {
                if (playerControlXbox.spellPrimary[i] == "Fire")
                {
                    spellUI[i].GetComponent<Image>().color = Color.red;
                    spellUI[i].GetComponent<Image>().sprite = fireSprite;
                }
                if (playerControlXbox.spellPrimary[i] == "Wind")
                {
                    spellUI[i].GetComponent<Image>().color = new Color32(67, 215, 255, 255);
                    spellUI[i].GetComponent<Image>().sprite = windSprite;
                }
                if (playerControlXbox.spellPrimary[i] == "Water")
                {
                    spellUI[i].GetComponent<Image>().color = Color.blue;
                    spellUI[i].GetComponent<Image>().sprite = waterSprite;
                }
                if (playerControlXbox.spellPrimary[i] == "")
                {
                    spellUI[i].GetComponent<Image>().color = Color.white;
                }
                if (playerControlXbox.spellSecondary[i] == "AOE")
                {
                    spellUI[i].GetComponent<Image>().sprite = aoeSprite;
                }
                if (playerControlXbox.spellSecondary[i] == "Range")
                {
                    spellUI[i].GetComponent<Image>().sprite = rangeSprite;
                }
                if (playerControlXbox.spellSecondary[i] == "Dash")
                {
                    if (i == 3)
                    {
                        spellUI[i].GetComponent<Image>().sprite = dashSpriteReverse;
                    }
                    else
                    {
                        spellUI[i].GetComponent<Image>().sprite = dashSprite;
                    }

                }
                if (playerControlXbox.spellPrimary[i] == "")
                {
                    spellUI[i].GetComponent<Image>().sprite = null;
                }

            }
        }

    }
}
