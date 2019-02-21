using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPlayerUI : MonoBehaviour
{


    public Image northImage;
    public Image eastImage;
    public Image southImage;
    public Image westImage;

    // Use this for initialization
    void Start()
    {
        northImage.alphaHitTestMinimumThreshold = 0.99f;
        eastImage.alphaHitTestMinimumThreshold = 0.99f;
        southImage.alphaHitTestMinimumThreshold = 0.99f;
        westImage.alphaHitTestMinimumThreshold = 0.99f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
