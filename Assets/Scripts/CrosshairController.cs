using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField]
    Color32 defaultColor;
    [SerializeField]
    Color32 hoverPickupColor;
    [SerializeField]
    Color32 hoverMassableColor;
    [SerializeField]
    Camera fpsCamera;
    [SerializeField]
    GameObject load;

    PlayerInteraction interact;

    void Start()
    {
        interact = PlayerManager.instance.Player.GetComponent<PlayerInteraction>();
    }
    
    void Update()
    {
        bool isHoveringPickup = false;
        bool isHoveringMassable = false;

        // If aiming at object..
        if(interact.HasHit)
        {
            GameObject targetedObject = interact.CurrentObject;
            // If aiming at a pickup or interactable that's within reach...
            if((targetedObject.GetComponent<Pickup>() ||  targetedObject.GetComponent<Interactable>()) &&
                !load.GetComponent<LoadManager>().PickupTooFar(targetedObject))
            {
                isHoveringPickup = true;
            }
            if(targetedObject.GetComponent<Massable>())
            {
                isHoveringMassable = true;
            }
        }


        // Set color of crosshair.
        if(!isHoveringMassable && isHoveringPickup)
        {
            GetComponent<Image>().color = hoverPickupColor;
        }
        else
        {
            GetComponent<Image>().color = defaultColor;
        }
        if(isHoveringMassable)
        {
            GetComponent<Image>().color = hoverMassableColor;
        }
    }
}
