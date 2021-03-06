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
    bool interact = false;
    bool firing = false;
    bool inRange = false;

    void Start()
    {
        InputManager.instance.Controls.Player.Interact.performed += ctx => OnInteract();
        InputManager.instance.Controls.Player.Fire.started += ctx => OnFire();
    }

    void Update()
    {
        inRange = false;

        var ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        RaycastHit hit = new RaycastHit();

        if(!isHolding && Physics.Raycast(ray, out hit, maxDistance))
        {
            currentPickup=hit.collider.gameObject; 
            if(!PickupTooFar() && currentPickup.GetComponent<Pickup>()) 
            {
                inRange = true;
            }
        }

        // Drop item if it's out of range.
        if(isHolding)
        {
            if(PickupTooFar())
            {
                PutDown();
            }
        }
    }

    void OnInteract()
    {
        if(!isHolding)
        {
            if(inRange)
            {
                currentPickup.GetComponent<Pickup>().PickUp(playerLoad);      
                isHolding = true;
            }
        }
        else
        {
            PutDown();
        }
    }

    void OnFire()
    {
        if(isHolding)
        {
            Throw();
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
