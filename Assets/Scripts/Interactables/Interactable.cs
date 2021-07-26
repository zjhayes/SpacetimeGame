using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void OnInteract();
    public OnInteract onInteract;

    public void Interact()
    {
        onInteract.Invoke();
    }
}
