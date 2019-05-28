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

    public Image image;

    public Sprite white;
    public Sprite red;
    public Sprite cyan;
    public Sprite blue;

    public GameObject childIcon;
    public GameObject outerRing;
    public GameObject innerRing;

    public Sprite cone;
    public Sprite line;
    public Sprite dash;
    public Sprite boom;

    private void Start()
    {
        playerControlXbox = player.GetComponent<PlayerControlXbox>();
        outerRing.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        innerRing.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    private void Update()
    {
        if (playerControlXbox.spellSelected == spellNumber)
        {
            image.enabled = true;
            childIcon.GetComponent<Image>().enabled = true;
            outerRing.SetActive(true);
            innerRing.SetActive(true);
            outerRing.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            innerRing.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            if (playerControlXbox.spellPrimary[spellNumber] == "Fire")
            {
                image.sprite = red;
                innerRing.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                childIcon.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            }
            if (playerControlXbox.spellPrimary[spellNumber] == "Wind")
            {
                image.sprite = cyan;
                //67, 215, 255, 255
                innerRing.GetComponent<Image>().color = new Color32(67, 215, 255, 255);
                childIcon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            if (playerControlXbox.spellPrimary[spellNumber] == "Water")
            {
                image.sprite = blue;
                innerRing.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
                childIcon.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
            }
            if (playerControlXbox.spellPrimary[spellNumber] == "Earth")
            {
                image.sprite = blue;
                innerRing.GetComponent<Image>().color = new Color32(90, 80, 0, 255);
                childIcon.GetComponent<Image>().color = new Color32(90, 80, 0, 255);
            }
            if (playerControlXbox.spellPrimary[spellNumber] == "")
            {
                image.sprite = white;
            }

            if (playerControlXbox.spellSecondary[spellNumber] == "AOE")
            {
                outerRing.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                childIcon.GetComponent<Image>().sprite = cone;
            }
            if (playerControlXbox.spellSecondary[spellNumber] == "Range")
            {
                outerRing.GetComponent<Image>().color = new Color32(67, 215, 255, 255);
                childIcon.GetComponent<Image>().sprite = line;
            }
            if (playerControlXbox.spellSecondary[spellNumber] == "Dash")
            {
                outerRing.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
                childIcon.GetComponent<Image>().sprite = dash;
            }
            if (playerControlXbox.spellSecondary[spellNumber] == "Boom")
            {
                outerRing.GetComponent<Image>().color = new Color32(90, 80, 0, 255);
                childIcon.GetComponent<Image>().sprite = boom;
            }
            if (playerControlXbox.spellSecondary[spellNumber] == "")
            {
                childIcon.GetComponent<Image>().sprite = null;
                childIcon.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }
        }
        else if (playerControlXbox.spellSelected != spellNumber)
        {
            image.enabled = false;
            childIcon.GetComponent<Image>().enabled = false;
            outerRing.SetActive(false);
            innerRing.SetActive(false);
        }

        

    }

}
