using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPlayerUISelectXbox : MonoBehaviour
{
    public bool selected = false;

    public GameObject player;
    public GameObject playerAim;
    public PlayerControlXbox playerControlXbox;
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

    private void Start()
    {
        playerControlXbox = player.GetComponent<PlayerControlXbox>();
    }

    private void Update()
    {
        if (playerControlXbox.spellPrimary[spellNumber] == "Fire")
        {
            but.image.sprite = red;
            childIcon.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
        if (playerControlXbox.spellPrimary[spellNumber] == "Wind")
        {
            but.image.sprite = cyan;
            //67, 215, 255, 255
            childIcon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (playerControlXbox.spellPrimary[spellNumber] == "Water")
        {
            but.image.sprite = blue;
            childIcon.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
        }
        if (playerControlXbox.spellPrimary[spellNumber] == "")
        {
            but.image.sprite = white;
        }

        if (playerControlXbox.spellSecondary[spellNumber] == "AOE")
        {
            childIcon.GetComponent<Image>().sprite = cone;
        }
        if (playerControlXbox.spellSecondary[spellNumber] == "Range")
        {
            childIcon.GetComponent<Image>().sprite = line;
        }
        if (playerControlXbox.spellSecondary[spellNumber] == "Dash")
        {
            childIcon.GetComponent<Image>().sprite = dash;
        }
        if (playerControlXbox.spellSecondary[spellNumber] == "")
        {
            childIcon.GetComponent<Image>().sprite = null;
            childIcon.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == playerAim)
        {
            selected = true;
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
