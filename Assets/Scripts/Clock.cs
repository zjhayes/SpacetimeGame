using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private CenterOfMass centerOfMass;

    void Start()
    {
        centerOfMass = MassManager.instance.CenterOfMass.GetComponent<CenterOfMass>();
    }

    public float TimeRelativeToPlayer()
    {
        if(centerOfMass.IsActive())
        {
            return centerOfMass.DistanceRelativeToPlayer(gameObject);
        }
        else
        {
            return 1.0f;
        }
    }
}
