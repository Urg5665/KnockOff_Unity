using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnPlayerUISelect : MonoBehaviour
{
    public bool selected = false;

    public void SpellSelect()
    {
        selected = true;
    }
    public void SpellDeselect()
    {
        selected = false;
    }


}
