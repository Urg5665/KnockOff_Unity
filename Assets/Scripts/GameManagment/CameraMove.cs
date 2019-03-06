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

    public float minZoom = 10f;
    public float maxZoom = 40f;

    public bool player1Hit;
    public bool player2Hit;

    public int hitTimer;

    public Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        bounds = new Bounds(targets[0].position, Vector3.zero);
    }

    Vector3 GetCenterPoint()
    {
       

        if (player1Hit)
        {
            //bounds = new Bounds(targets[0].position, Vector3.zero);
            //bounds.Encapsulate(targets[0].position);
            bounds.center = player1.transform.position;
            return bounds.center;
        }
        if (player2Hit)
        {
            //bounds = new Bounds(targets[1].position, Vector3.zero);
            //bounds.Encapsulate(targets[1].position);
            bounds.center = player2.transform.position;
            return bounds.center;
        }

        else
        {
            bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 0; i < targets.Count; i++)
            {
                bounds.Encapsulate(targets[i].position);
            }
            return bounds.center;
        }

    }

    void LateUpdate()
    {
        //if (targets[1].position.x == 250)
        //{
          //  return;
        //}
        Move();
        if ( !player1Hit && !player2Hit)
        {
            //Zoom();
        }


        if (Input.GetKeyDown(KeyCode.G)){
            StartCoroutine(Shake(.15f, .4f));
        } 

        if ( player1Hit || player2Hit)
        {
            hitTimer++;
            if (hitTimer < 20)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, cam.fieldOfView - 5, Time.deltaTime);
            if (hitTimer > 50)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, cam.fieldOfView + 5, Time.deltaTime);
        }
        if (hitTimer == 70)
        {
            hitTimer = 0;
            player2Hit = false;
            player1Hit = false;
        }

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

    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while( elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;

    }


}
