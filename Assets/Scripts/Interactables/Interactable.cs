using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    float maxDistance = 3.0f;
    public delegate void OnInteract();
    public OnInteract onInteract;

    public void Interact()
    {
        onInteract.Invoke();
    }

    public float MaxDistance
    {
        get{ return maxDistance; }
        set{ maxDistance = value; }
    }
}
