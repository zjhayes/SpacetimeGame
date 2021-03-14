using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;

    public float DistanceRelativeToPlayer(GameObject other)
    {
        float distanceToObject = Vector3.Distance(this.transform.position, other.transform.position);
        float distanceToPlayer = Vector3.Distance(this.transform.position, PlayerManager.instance.Player.transform.position);

        return distanceToObject / distanceToPlayer;
    }

    public Vector3 Offset
    {
        get { return offset; }
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
