using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
    [SerializeField]
    float maxLife = 2.0f;
    float destroyDelay = .05f;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PhotonSensor>())
        {
            other.GetComponent<PhotonSensor>().Power();
        }

        if(other.GetComponent<Photon>())
        {
            return; // Ignore other photons.
        }

        Destroy(gameObject, destroyDelay);
    }

    void Start()
    {
        // Destroy after life expires.
        Destroy(gameObject, maxLife);
    }
}
