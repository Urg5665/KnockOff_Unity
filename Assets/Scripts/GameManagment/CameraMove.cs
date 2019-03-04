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

    void LateUpdate()
    {
        if (targets[1].position.x == 250)
        {
            return;
        }
        Move();
        Zoom();
        if (Input.GetKeyDown(KeyCode.G)){
            StartCoroutine(Shake(.15f, .4f));
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
