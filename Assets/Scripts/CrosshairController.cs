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
        var ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        RaycastHit hit = new RaycastHit();

        bool isHovering = false;

        if(Physics.Raycast(ray, out hit, maxDistance))
        {   
            if(hit.collider.gameObject.GetComponent<Pickup>())
            {
                isHovering = true;
            }
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
