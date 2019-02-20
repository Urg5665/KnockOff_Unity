using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera cam;

    public Transform player1;
    public Transform player2;

    public List<Transform> targets;

    public Vector3 offset;

    private Vector3 velocity;

    public float smoothTime = .5f;

    public float minZoom = 15f;
    public float maxZoom = 40f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;

        

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (targets[1].position.x == 250)
        {
            return;
        }

        Move();
        Zoom();

    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / 50f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        GetGreatestDistance();
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for( int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }


    private void Move() 
    {

        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPostion = centerPoint + offset;

        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, newPostion, ref velocity, smoothTime);
    }
}
