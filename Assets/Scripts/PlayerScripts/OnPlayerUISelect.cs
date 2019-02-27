using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnPlayerUISelect : MonoBehaviour
{
    public bool selected = false;

    public GameObject player;
    public PlayerControl playerControl;
    public int spellNumber;

    public Image image;

    public Sprite white;
    public Sprite red;
    public Sprite cyan;
    public Sprite blue;

    public GameObject childIcon;

    public Sprite cone;
    public Sprite line;
    public Sprite dash;

    private void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        if (playerControl.spellSelected == spellNumber)
        {
            image.enabled = true;
            childIcon.GetComponent<Image>().enabled = true;

            if (playerControl.spellPrimary[spellNumber] == "Fire")
            {
                image.sprite = red;
                childIcon.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            }
            if (playerControl.spellPrimary[spellNumber] == "Wind")
            {
                image.sprite = cyan;
                //67, 215, 255, 255
                childIcon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            if (playerControl.spellPrimary[spellNumber] == "Water")
            {
                image.sprite = blue;
                childIcon.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
            }
            if (playerControl.spellPrimary[spellNumber] == "")
            {
                image.sprite = white;
            }

            if (playerControl.spellSecondary[spellNumber] == "AOE")
            {
                childIcon.GetComponent<Image>().sprite = cone;
            }
            if (playerControl.spellSecondary[spellNumber] == "Range")
            {
                childIcon.GetComponent<Image>().sprite = line;
            }
            if (playerControl.spellSecondary[spellNumber] == "Dash")
            {
                childIcon.GetComponent<Image>().sprite = dash;
            }
            if (playerControl.spellSecondary[spellNumber] == "")
            {
                childIcon.GetComponent<Image>().sprite = null;
                childIcon.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }
        }
        else if (playerControl.spellSelected != spellNumber)
        {
            image.enabled = false;
            childIcon.GetComponent<Image>().enabled = false;
        }
    }
}