﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [SerializeField]
    float throwForce = 600.0f;
    [SerializeField]
    float dropForce = 50.0f;

    GameObject currentPickup;
    PlayerInteraction interact;

    bool recentlyThrown = false;

    void Start()
    {
        interact = PlayerManager.instance.Player.GetComponent<PlayerInteraction>();
        interact.onInteract += OnInteract;
    }

    void Update()
    {
        // Drop item if it's out of range.
        if(HasLoad())
        {
            if(PickupTooFar())
            {
                PutDown();
            }
        }

        recentlyThrown = false;
    }

    void OnInteract()
    {
        if(!HasLoad() && interact.HasHit)
        {
            // Check if object being aimed at can be picked up.
            GameObject objectInView = interact.CurrentObject;
            if(objectInView.GetComponent<Pickup>())
            {
                PickUp(objectInView);
            }

            // Call interaction event.
            if(objectInView.GetComponent<Interactable>())
            {
                objectInView.GetComponent<Interactable>().Interact();
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
    }

    public void PutDown()
    {
        currentPickup.GetComponent<Pickup>().Throw(gameObject, dropForce);
        currentPickup = null;
    }

    public void Throw()
    {
        if(HasLoad())
        {
            currentPickup.GetComponent<Pickup>().Throw(gameObject, throwForce);
            currentPickup = null;
            recentlyThrown = true;
        }
    }

    bool PickupTooFar()
    {
        return PickupTooFar(currentPickup);
    }

    public bool PickupTooFar(GameObject compare)
    {
        return DistanceToLoad(compare) > PlayerManager.instance.Reach;
    }

    float DistanceToLoad(GameObject compare)
    {
        return Vector3.Distance(compare.transform.position, transform.position);
    }

    public bool HasLoad()
    {
        if((currentPickup == null || !currentPickup.GetComponent<Pickup>().IsHolding))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool RecentlyThrown()
    {
        return recentlyThrown;
    }

    public GameObject CurrentPickup
    {
        get{ return currentPickup; }
    }
}
