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

        GameObject centerOfMass = MassManager.instance.CenterOfMass;
        if(!centerOfMass.GetComponent<CenterOfMass>().IsActive()) 
        { 
            // Set to default gravity.
            gameObject.GetComponent<Rigidbody>().useGravity = useGravityDefault;
            return; 
        }

        float distance = Vector3.Distance(centerOfMass.transform.position, gameObject.transform.position);

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
        Vector3 desiredDirection = (centerOfMass.transform.position + centerOfMass.GetComponent<CenterOfMass>().Offset - gameObject.transform.position).normalized;
        Vector3 newVelocity = Vector3.RotateTowards(gameObject.GetComponent<Rigidbody>().velocity, desiredDirection,
            rotationSpeedDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);

        gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
    }

    public float Degrees
    {
        get{ return degrees; }
        set{ degrees = value; }
    }
}
