using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField]
    Color32 defaultColor;
    [SerializeField]
    Color32 interactColor;
    [SerializeField]
    Camera fpsCamera;
    [SerializeField]
    TooltipController tooltip;

    PlayerInteraction interact;

    void Start()
    {
        interact = PlayerManager.instance.Player.GetComponent<PlayerInteraction>();
        GetComponent<Image>().color = defaultColor;
    }
    
    void Update()
    {
        tooltip.HideTooltip();

        // If aiming at object..
        if(interact.HasHit)
        {
            GameObject targetedObject = interact.CurrentObject;

            // Show Tooltip
            if(targetedObject.GetComponent<Tooltip>())
            {
                string tooltipText = targetedObject.GetComponent<Tooltip>().TooltipText;
                tooltip.ShowTooltip(tooltipText);
            }

            if(targetedObject.GetComponent<Interactable>())
            {
                GetComponent<Image>().color = interactColor;
            }
            else
            {
                GetComponent<Image>().color = defaultColor;
            }
        }
    }
}
