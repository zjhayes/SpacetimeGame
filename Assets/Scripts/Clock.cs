using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private GameObject centerOfMass;

    void Start()
    {
        centerOfMass = MassManager.instance.CenterOfMass;
    }

    public float TimeRelativeToPlayer
    {
        get
        {
            if(centerOfMass == null)
            {
                return 1.0f;
            }
            else
            {
                return centerOfMass.GetComponent<CenterOfMass>().DistanceRelativeToPlayer(gameObject);
            }
        }
    }
}
