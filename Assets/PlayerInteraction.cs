using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    Camera fpsCamera;
    [SerializeField]
    GameObject playerLoad;
    [SerializeField]
    float maxDistance = 500.0f;
    [SerializeField]
    float throwForce = 600.0f;
    [SerializeField]
    float maxHoldDistance = 1.0f;

    GameObject currentPickup;
    bool isHolding;

    void Update()
    {
        if(isHolding)
        {
            if(PickupTooFar())
            {
                PutDown();
            }
        }

        if (Input.GetKeyDown("e"))
        {
            if(!isHolding)
            {
                PickUp();
            }
            else
            {
                PutDown();
            }
        }

        if(Input.GetKeyDown("f"))
        {
            if(isHolding)
            {
                Throw();
            }
        }
    }

    void PickUp()
    {
        var ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            currentPickup=hit.collider.gameObject; 
            if(!PickupTooFar()) 
            {
                if(currentPickup.GetComponent<Pickup>())
                {
                    currentPickup.GetComponent<Pickup>().PickUp(playerLoad);      
                    isHolding = true;
                }
            }
        }
    }

    void PutDown()
    {
        currentPickup.GetComponent<Pickup>().PutDown();
        currentPickup = null;
        isHolding = false;
    }

    void Throw()
    {
        currentPickup.GetComponent<Pickup>().Throw(playerLoad, throwForce);
        currentPickup = null;
        isHolding = false;
    }

    bool PickupTooFar()
    {
        float distance = Vector3.Distance(currentPickup.transform.position, playerLoad.transform.position);
        return distance > maxHoldDistance;
    }
}
