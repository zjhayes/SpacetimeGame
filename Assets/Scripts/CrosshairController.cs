using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
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

            if(targetedObject.GetComponent<Interactable>() && 
                interact.DistanceToHit() <= targetedObject.GetComponent<Interactable>().MaxDistance)
            {
                Debug.Log(interact.DistanceToHit());
                GetComponent<Image>().color = interactColor;
                
                // Show Tooltip
                if(targetedObject.GetComponent<Tooltip>())
                {
                    string tooltipText = targetedObject.GetComponent<Tooltip>().TooltipText;
                    tooltip.ShowTooltip(tooltipText);
                }
            }
            else
            {
                GetComponent<Image>().color = defaultColor;
            }
        }
    }
}
