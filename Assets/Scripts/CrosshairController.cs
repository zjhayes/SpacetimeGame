using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField]
    Color32 defaultColor;
    [SerializeField]
    Color32 hoverColor;
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

        // If aiming at object, if it's close enough to pick up...
        if(currentTarget != null && currentTarget.GetComponent<Pickup>() && !load.GetComponent<LoadManager>().PickupTooFar(currentTarget))
        {
            isHoveringPickup = true;
        }

        // Set color of crosshair.
        if(isHoveringPickup)
        {
            GetComponent<Image>().color = hoverColor;
        }
        else
        {
            GetComponent<Image>().color = defaultColor;
        }
    }
}
