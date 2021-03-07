using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
    [SerializeField]
    float maxLife = 2.0f;
    LineRenderer frontLaser;
    LineRenderer backLaser;
    float destroyDelay = .05f;

    public LineRenderer FrontLaser
    {
        set { frontLaser = value; }
    }

    public LineRenderer BackLaser
    {
        set { backLaser = value; }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, destroyDelay);
    }

    void Start()
    {
        Destroy(gameObject, maxLife);
    }

    void Update()
    {
        if(frontLaser != null)
        {
            frontLaser.SetPosition(1, transform.position);
        }
        if(backLaser != null)
        {
            backLaser.SetPosition(0, transform.position);
        }
    }
}
