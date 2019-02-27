using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnPlayerUISelect : MonoBehaviour
{
    public bool selected = false;

    public GameObject player1;
    public int spellNumber;

    public Button but;

    public Sprite white;
    public Sprite red;
    public Sprite cyan;
    public Sprite blue;

    public GameObject childIcon;

    public Sprite cone;
    public Sprite line;
    public Sprite dash;

    private void Update()
    {
        if (player1.GetComponent<PlayerControl>().spellPrimary[spellNumber] == "Fire")
        {
            but.image.sprite = red;
            childIcon.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
        if (player1.GetComponent<PlayerControl>().spellPrimary[spellNumber] == "Wind")
        {
            but.image.sprite = cyan;
            //67, 215, 255, 255
            childIcon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (player1.GetComponent<PlayerControl>().spellPrimary[spellNumber] == "Water")
        {
            but.image.sprite = blue;
            childIcon.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
        }
        if (player1.GetComponent<PlayerControl>().spellPrimary[spellNumber] == "")
        {
            but.image.sprite = white;
        }

        if (player1.GetComponent<PlayerControl>().spellSecondary[spellNumber] == "AOE")
        {
            childIcon.GetComponent<Image>().sprite = cone;
        }
        if (player1.GetComponent<PlayerControl>().spellSecondary[spellNumber] == "Range")
        {
            childIcon.GetComponent<Image>().sprite = line;
        }
        if (player1.GetComponent<PlayerControl>().spellSecondary[spellNumber] == "Dash")
        {
            childIcon.GetComponent<Image>().sprite = dash;
        }
        if (player1.GetComponent<PlayerControl>().spellSecondary[spellNumber] == "")
        {
            childIcon.GetComponent<Image>().sprite = null;
            childIcon.GetComponent<Image>().color = new Color32(255,255,255,0);
        }

    }       

    public void SpellSelect()
    {
        selected = true;
    }
    public void SpellDeselect()
    {
        selected = false;
    }


}
