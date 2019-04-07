using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavoir : MonoBehaviour
{
    public bool destroyed;
    public int destroyTimer;

    public MeshRenderer mesh;
    public MeshCollider col;

    // Start is called before the first frame update
    void Start()
    {
        mesh = this.GetComponent<MeshRenderer>();
        col = this.GetComponentInChildren<MeshCollider>();
        destroyed = false;
        destroyTimer = 0;
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "earthQuake")
        {
            Debug.Log(this.gameObject.name + " was Destroyed");
            destroyed = true;
        }
    }
    

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.H))
        {
            destroyed = true;
        }

        if (destroyed)
        {
            mesh.enabled = false;
            col.enabled = false;
            destroyTimer++;
        }
        if (destroyTimer > 200)
        {
            destroyed = false;
            destroyTimer = 0;
            mesh.enabled = true;
            col.enabled = true;
        }



    }
}
