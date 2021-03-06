using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [SerializeField]
    float throwForce = 600.0f;
    [SerializeField]
    float dropForce = 50.0f;
    [SerializeField]
    float maxHoldDistance = 1.0f;

    GameObject currentPickup;
    bool isHolding = false;

    void Start()
    {
        PlayerManager.instance.Player.GetComponent<PlayerInteraction>().onInteract += OnInteract;
        PlayerManager.instance.Player.GetComponent<PlayerInteraction>().onFire += Throw;
    }

    void Update()
    {
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
            // Check if object being aimed at can be picked up.
            GameObject objectInView = PlayerManager.instance.Player.GetComponent<PlayerInteraction>().CurrentHit;
            if(objectInView.GetComponent<Pickup>())
            {
                PickUp(objectInView);
            }
        }
        else
        {
            PutDown();
        }
    }

    void PickUp(GameObject objectInView)
    {
        currentPickup = objectInView;
        currentPickup.GetComponent<Pickup>().PickUp(gameObject);      
        isHolding = true;
    }

    void PutDown()
    {
        currentPickup.GetComponent<Pickup>().Throw(gameObject, dropForce);
        currentPickup = null;
        isHolding = false;
    }

    void Throw()
    {
        if(isHolding)
        {
            currentPickup.GetComponent<Pickup>().Throw(gameObject, throwForce);
            currentPickup = null;
            isHolding = false;
        }
    }

    bool PickupTooFar()
    {
        float distance = Vector3.Distance(currentPickup.transform.position, transform.position);
        return distance > maxHoldDistance;
    }
}
