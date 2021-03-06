using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField]
    float maxDistance = 500.0f;
    [SerializeField]
    Color32 defaultColor;
    [SerializeField]
    Color32 hoverColor;
    [SerializeField]
    Camera fpsCamera;
    
    void Update()
    {
        GameObject currentTarget = PlayerManager.instance.Player.GetComponent<PlayerInteraction>().CurrentHit;

        bool isHovering = false;

        if(currentTarget != null && currentTarget.GetComponent<Pickup>())
        {
            isHovering = true;
        }

        if(isHovering)
        {
            GetComponent<Image>().color = hoverColor;
        }
        else
        {
            GetComponent<Image>().color = defaultColor;
        }
    }
}
