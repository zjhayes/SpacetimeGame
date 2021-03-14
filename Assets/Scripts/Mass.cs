using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Mass : MonoBehaviour
{
    [SerializeField]
    float gravityRadius = 4.0f;
    [SerializeField]
    float degrees = 360;
    [SerializeField]
    bool useGravityDefault = true;

    void Update()
    {
        // Return if holding.
        if(gameObject.GetComponent<Pickup>() && gameObject.GetComponent<Pickup>().IsHolding)
        {
            return;
        }

        if(!MassManager.instance.HasMassTransforms()) 
        { 
            // Set to default gravity.
            gameObject.GetComponent<Rigidbody>().useGravity = useGravityDefault;
            return; 
        }

        // Cycle through objects with mass, and adjust velocity with gravity.
        foreach(Transform massable in MassManager.instance.MassTransforms)
        {
            float distance = Vector3.Distance(massable.position, gameObject.transform.position);

            // Disable gravity if within gravityRadius.
            if(distance > gravityRadius)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = useGravityDefault;
            }
            else
            { 
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }

            float rotationSpeedDegrees = (1 / distance) * degrees; // degrees per second.
            Vector3 desiredDirection = (massable.position + massable.GetComponent<Massable>().Offset - gameObject.transform.position).normalized;
            Vector3 newVelocity = Vector3.RotateTowards(gameObject.GetComponent<Rigidbody>().velocity, desiredDirection,
                rotationSpeedDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);

            gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
        }
    }

    public float Degrees
    {
        get{ return degrees; }
        set{ degrees = value; }
    }

    // TODO: Add center of mass calculation.
    /**
    private Vector3 CalculateCenterOfMass()
    {
        Vector3 centroid = new Vector3();
        float massTotal = 0.0f;
        // Find center position of all mass objects, weighted by mass.
        foreach(Transform massObject in massTransforms)
        {
            float mass = massObject.gameObject.GetComponent<Weighted>().Weight;
            
            centroid += massObject.transform.position * mass;
            massTotal += mass;
        }
        centroid /= massTotal;
        
        return centroid;
    }*/
}
