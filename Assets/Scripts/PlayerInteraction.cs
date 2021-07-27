using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    Camera fpsCamera;
    [SerializeField]
    float maxDistance = 500.0f;

    bool hasHit = false;
    GameObject currentObject;
    Vector3 currentPoint;


    //public delegate void OnInteract();
    //public OnInteract onInteract;

    void Start()
    {
        InputManager.instance.Controls.Player.Interact.performed += ctx => Interact();
    }

    void Update()
    {
        var ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            if(hit.transform.gameObject != null && !hit.transform.GetComponent<IgnoreInteraction>())
            {
                currentObject = hit.transform.gameObject;
            }
            currentPoint = hit.point;
            hasHit = true;
        }
        else
        {
            hasHit = false;
        }
    }

    public GameObject CurrentObject
    {
        get{ return currentObject; }
    }

    public Vector3 CurrentPoint
    {
        get{ return currentPoint; }
    }

    public bool HasHit
    {
        get{ return hasHit; }
    }

    void Interact()
    {
        if(currentObject.GetComponent<Interactable>())
        {
            currentObject.GetComponent<Interactable>().Interact();
        }
    }

    public float DistanceToHit()
    {
        float distance = Vector3.Distance(currentObject.transform.position, transform.position);
        return distance;
    }

    public bool IsFacing(GameObject other, float degreesOfFreedom)
    {
        return Vector3.Angle(fpsCamera.transform.forward, other.transform.forward) > degreesOfFreedom;
    }
}