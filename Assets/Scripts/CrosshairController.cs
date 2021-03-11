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
        GameObject currentTarget = interact.CurrentHit;

        bool isHoveringPickup = false;
        bool isHoveringMassable = false;

        // If aiming at object, if it's close enough to pick up...
        if(currentTarget != null)
        {
            if(currentTarget.GetComponent<Pickup>() && !load.GetComponent<LoadManager>().PickupTooFar(currentTarget))
            {
                isHoveringPickup = true;
            }
            if(currentTarget.GetComponent<CenterOfMass>())
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
