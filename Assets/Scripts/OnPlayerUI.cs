using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnPlayerUI : MonoBehaviour
{
    public bool selected = true;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            selected = true;
        }
        else
        {
            selected = false;
        }
    }


}
