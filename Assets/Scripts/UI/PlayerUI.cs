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
    }

    void Update()
    {
        if (playerBelong.name == "Player1")
        {
            for (int i = 0; i < 4; i++)
            {
                if (playerControl.spellPrimary[i] == "Fire")
                {
                    spellUI[i].GetComponent<Image>().color = Color.red;
                }
                if (playerControl.spellPrimary[i] == "Wind")
                {
                    spellUI[i].GetComponent<Image>().color = new Color32(67, 215, 255, 255);
                }
                if (playerControl.spellPrimary[i] == "Water")
                {
                    spellUI[i].GetComponent<Image>().color = Color.blue;
                }
                if (playerControl.spellPrimary[i] == "")
                {
                    spellUI[i].GetComponent<Image>().color = Color.white;
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
                    if (i == 3)
                    {
                        spellUI[i].GetComponent<Image>().sprite = dashSpriteReverse;
                    }
                    else
                    {
                        spellUI[i].GetComponent<Image>().sprite = dashSprite;
                    }

                }
                if (playerControl.spellSecondary[i] == "")
                {
                    spellUI[i].GetComponent<Image>().sprite = null;
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
                }
                if (playerControlXbox.spellPrimary[i] == "Wind")
                {
                    spellUI[i].GetComponent<Image>().color = new Color32(67, 215, 255, 255);
                }
                if (playerControlXbox.spellPrimary[i] == "Water")
                {
                    spellUI[i].GetComponent<Image>().color = Color.blue;
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
                if (playerControlXbox.spellSecondary[i] == "")
                {
                    spellUI[i].GetComponent<Image>().sprite = null;
                }

            }
        }

    }
}
