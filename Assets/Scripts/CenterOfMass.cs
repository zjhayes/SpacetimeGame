using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CenterOfMass : MonoBehaviour
{
    void Update()
    {
        
    }

    public float DistanceRelativeToPlayer(GameObject other)
    {
        float distanceToObject = Vector3.Distance(this.transform.position, other.transform.position);
        float distanceToPlayer = Vector3.Distance(this.transform.position, PlayerManager.instance.Player.transform.position);

        return distanceToObject / distanceToPlayer;
    }
}
