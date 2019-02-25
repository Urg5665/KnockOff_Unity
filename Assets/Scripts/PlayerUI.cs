using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject playerBelong;
    public PlayerControl playerControl;

    public GameObject[] spellUI;

    public Sprite aoeSprite;
    public Sprite rangeSprite;
    public Sprite dashSprite;

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
                spellUI[i].GetComponent<Image>().color = Color.red;
            }
            if (playerControl.spellPrimary[i] == "Wind")
            {
                spellUI[i].GetComponent<Image>().color = new Color32(67,215,255,255);
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
                spellUI[i].GetComponent<Image>().sprite = dashSprite;
            }
            if (playerControl.spellSecondary[i] == "")
            {
                spellUI[i].GetComponent<Image>().sprite = null;
            }

        }
    }
}
