using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonCanon : MonoBehaviour
{
    [SerializeField]
    GameObject photonPrefab;
    [SerializeField]
    float speed = 600.0f;
    [SerializeField]
    float laserWidth = .2f;
    [SerializeField]
    float maxPhotonDistance = 5f;
    [SerializeField]
    float fireRate = 0.1f;
    List<GameObject> photons;
    LineRenderer line;

     private float nextActionTime = 0.0f;

    void Start()
    {
        photons = new List<GameObject>();
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {

     if (Time.time > nextActionTime) 
     {
        nextActionTime += fireRate;
        Fire();
     }
        UpdateLaser();
    }

    void UpdateLaser()
    {
        List<Vector3> points = new List<Vector3>();
        GameObject previousPhoton = null;

        List<GameObject> photonsToRemove = new List<GameObject>();

        foreach(GameObject photon in photons)
        {
            if(photon == null)
            {
                photonsToRemove.Add(photon);
            }
            else
            {
                // Remove photons that are too distant.
                if(previousPhoton != null && Vector3.Distance(photon.transform.position, previousPhoton.transform.position) > maxPhotonDistance)
                {
                    photonsToRemove.Add(previousPhoton);
                }

                points.Add(photon.transform.position);
                previousPhoton = photon;
            }
        }

        // Remove destroyed photons and all proceeding photons.
        foreach(GameObject destroyedPhoton in photonsToRemove)
        {
            photons.IndexOf(destroyedPhoton);
            for(int i = photons.IndexOf(destroyedPhoton); i > 0; i--)
            {
                photons.RemoveAt(i);
            }
        }

        // Set laser line position to photons.
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }

    void Fire()
    {
        GameObject newPhoton = Instantiate(photonPrefab, (this.transform.position + transform.forward), Quaternion.identity);
        newPhoton.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
        photons.Add(newPhoton);
    }
}
