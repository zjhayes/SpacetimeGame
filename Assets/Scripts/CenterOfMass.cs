using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CenterOfMass : MonoBehaviour
{
    [SerializeField]
    bool setDefault = false;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    float fadeSpeed = 1.0f;

    public delegate void OnMassChanged();
    public OnMassChanged onMassChanged;


    void Start()
    {
        if(setDefault)
        {
            // Set this as default center of mass.
            Set();
        }
    }

    public void Set()
    {
        MassManager.instance.CenterOfMass = gameObject;
        
        if(onMassChanged != null)
        {
            onMassChanged.Invoke();
        }
    }

    public void Unset()
    {
        MassManager.instance.CenterOfMass = null;

        if(onMassChanged != null)
        {
            onMassChanged.Invoke();
        }
    }

    public bool IsSet
    {
        get
        { 
            return MassManager.instance.CenterOfMass == gameObject; 
        }
    }

    public Vector3 Offset
    {
        get { return offset; }
    }

    public float DistanceRelativeToPlayer(GameObject other)
    {
        float distanceToObject = Vector3.Distance(this.transform.position, other.transform.position);
        float distanceToPlayer = Vector3.Distance(this.transform.position, PlayerManager.instance.Player.transform.position);

        return distanceToObject / distanceToPlayer;
    }
}
