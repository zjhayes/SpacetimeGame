﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    float distanceFromMass = 1;
    private GameObject centerOfMass;

    void Start()
    {
        centerOfMass = MassManager.instance.CenterOfMass;
    }

    public float TimeRelativeToPlayer
    {
        get
        {
            return centerOfMass.GetComponent<Mass>().DistanceRelativeToPlayer(gameObject);
        }
    }

    public float DistanceFromMass
    {
        get { return distanceFromMass; }
        set { distanceFromMass = value; }
    }
}