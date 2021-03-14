using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float TimeRelativeToPlayer()
    {
        if(MassManager.instance.HasMassTransforms())
        {
            float distanceSum = 0.0f;
            foreach(Transform massable in MassManager.instance.MassTransforms)
            {
                distanceSum += massable.GetComponent<Massable>().DistanceRelativeToPlayer(gameObject);
            }
            // Return average distance from clock to mass objects.
            return distanceSum / MassManager.instance.MassTransforms.Count;
        }
        else
        {
            // No time dilation.
            return 1.0f;
        }
    }
}
