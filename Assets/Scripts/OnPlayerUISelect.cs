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

    private void Update()
    {
        if (player1.GetComponent<PlayerControl>().spellPrimary[spellNumber] == "Fire")
        {
            but.image.sprite = red;
        }
        if (player1.GetComponent<PlayerControl>().spellPrimary[spellNumber] == "Wind")
        {
            but.image.sprite = cyan;
        }
        if (player1.GetComponent<PlayerControl>().spellPrimary[spellNumber] == "Water")
        {
            but.image.sprite = blue;
        }
        if (player1.GetComponent<PlayerControl>().spellPrimary[spellNumber] == "")
        {
            but.image.sprite = white;
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
