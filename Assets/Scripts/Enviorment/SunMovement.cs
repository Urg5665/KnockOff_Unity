using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public int timeFrames;
    public float[] rgbDay;
    public float[] rgbNight;

    public Light globalLight;

    public float red;
    public float blue;
    public float green;
    public float redDif;

    // Start is called before the first frame update
    void Start()
    {
        timeFrames = 0;
        rgbDay = new float[] { 255, 189, 88 };
        rgbNight = new float[] {0, 20, 137};
        globalLight.color =  new Color32(255, 189, 88, 255);
        red = 255;
        green = 189;
        blue = 88;
        redDif = 255 / 2200;
    }

    // Update is called once per frame
    void Update()
    {
            // Spin the object around the world origin at 20 degrees/second.
        transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime); // origianl was 10
        timeFrames++;
        //print(timeFrames); // If you want conditional lighting you have to base it off of the current spend and ajdust accordingly
        //print(Mathf.Floor(1.002f));

        if ( 1200 < timeFrames && timeFrames < 3200) // day
        {
            //print("Daytime :" + timeFrames);
            //print(red + "  " + redDif);
            if ( red > 0)
            {
                red -= 0.2f;
            }
            if (green > 20)
            {
                green -= 0.2f;
            }
            if (blue < 137)
            {
                blue += 0.05f;
            }
        }
        else if (timeFrames >= 3200) // night
        {
            //print("Nightime :" + timeFrames);
            if (red <= 255)
            {
                red += 0.2f;
            }
            if (green < 189)
            {
                green += 0.2f;
            }
            if (blue > 88)
            {
                blue -= 0.05f;
            }
        }
        if (timeFrames == 5600)
        {
            timeFrames = 1200;
            red = 255;
            green = 189;
            blue = 88;

        }
        globalLight.color = new Color32( (byte)red, (byte)green, (byte)blue, 255);
        print(((byte)red, (byte)green, (byte)blue, 255) + "  tiemframes " + timeFrames);

    }
}
