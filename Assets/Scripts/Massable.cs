using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Massable : MonoBehaviour
{
    [SerializeField]
    bool hasMass = false;

    public delegate void OnMassChanged();
    public OnMassChanged onMassChanged;
    [SerializeField]
    Vector3 offset; // Offset the center of gravity.

    void Start()
    {
        if(hasMass)
        {
            // Has mass by default.
            Set();
        }
    }

    public void Set()
    {
        MassManager.instance.AddMassTransform(this.transform);
        hasMass = true;
        InvokeOnMassChanged();
    }

    public void Unset()
    {
        MassManager.instance.RemoveMassTransform(this.transform);
        hasMass = false;
        InvokeOnMassChanged();
    }

    public float DistanceRelativeToPlayer(GameObject other)
    {
        float distanceToObject = Vector3.Distance(this.transform.position, other.transform.position);
        float distanceToPlayer = Vector3.Distance(this.transform.position, PlayerManager.instance.Player.transform.position);

        return distanceToObject / distanceToPlayer;
    }

    public bool HasMass
    {
        get{ return hasMass; }
        set{ hasMass = value; }
    }

    public Vector3 Offset
    {
        get { return offset; }
    }

    private void InvokeOnMassChanged()
    {
        if(onMassChanged != null)
        {
            onMassChanged.Invoke();
        }
    }
}
