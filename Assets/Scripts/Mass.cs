using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Mass : MonoBehaviour
{
    [SerializeField]
    float degrees = 360;
    [SerializeField]
    bool useWorldGravity = true; // Object has gravity.
    [SerializeField]
    float gravityRadius = 30.0f; // How far from a massable this object is influenced by its gravity.
    [SerializeField]
    float worldGravityDegrees = 90.0f; // Degrees object rotates towards gravity source.

    void Update()
    {
        // Return if holding.
        if(gameObject.GetComponent<Pickup>() && gameObject.GetComponent<Pickup>().IsHolding)
        {
            return;
        }

        float distanceSum = 0.0f;

        // Cycle through objects with mass, and adjust velocity with gravity.
        foreach(Transform massable in MassManager.instance.MassTransforms)
        {
            float distance = Vector3.Distance(massable.position, gameObject.transform.position);
            distanceSum += distance;

            float rotationSpeedDegrees = (1 / distance) * degrees; // degrees per second.
            Vector3 desiredDirection = (massable.position + massable.GetComponent<Massable>().Offset - gameObject.transform.position).normalized;
            Vector3 newVelocity = Vector3.RotateTowards(gameObject.GetComponent<Rigidbody>().velocity, desiredDirection,
                rotationSpeedDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);

            gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
        }
        if(useWorldGravity)
        {
            float massableCount = MassManager.instance.MassTransforms.Count;
            float averageDistanceFromMassables = distanceSum/massableCount;
            // Calculate gravity power using the distance of the object from the massable object and its gravity radius.
            float gravityFactor = (averageDistanceFromMassables > 0 && averageDistanceFromMassables < gravityRadius) ? averageDistanceFromMassables/gravityRadius : 1.0f;
            FactorWorldGravity(gravityFactor);
        }
    }

    void FactorWorldGravity(float gravityFactor)
    {
        GameObject worldGravity = MassManager.instance.WorldGravity;

        float rotationSpeedDegrees = gravityFactor * worldGravityDegrees; // degrees per second.
        
        Vector3 desiredDirection = (worldGravity.transform.position - gameObject.transform.position).normalized;
        Vector3 newVelocity = Vector3.RotateTowards(gameObject.GetComponent<Rigidbody>().velocity, desiredDirection,
            rotationSpeedDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);

        gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
        Vector3 worldGravityForce = Vector3.down * gameObject.GetComponent<Weighted>().Weight * gravityFactor;
        gameObject.GetComponent<Rigidbody>().AddForce(worldGravityForce);
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
