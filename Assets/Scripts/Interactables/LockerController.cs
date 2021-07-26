using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class LockerController : MonoBehaviour
{
    [SerializeField]
    Interactable interaction;

    void Start()
    {
        interaction = gameObject.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
            // Call interaction event.
            // if(objectInView.GetComponent<Interactable>())
            // {
            //     objectInView.GetComponent<Interactable>().Interact();
            // }
    }
}
