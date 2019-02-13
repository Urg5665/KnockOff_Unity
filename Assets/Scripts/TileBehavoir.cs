using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavoir : MonoBehaviour
{
    public Material mouseOn;
    public Material storeMaterial;

    public GameObject[] occupyingObjects;

    public static GameObject tileSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.GetComponent<Renderer>().material = mouseOn;
            tileSelected = this.gameObject;
            //Debug.Log(this.gameObject.name);

        }
    }
    void OnMouseExit()
    {
        this.GetComponent<Renderer>().material = storeMaterial;
    }
}
