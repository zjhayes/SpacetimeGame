using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField]
    Color32 defaultColor;
    [SerializeField]
    Camera fpsCamera;

    PlayerInteraction interact;

    void Start()
    {
        interact = PlayerManager.instance.Player.GetComponent<PlayerInteraction>();
        GetComponent<Image>().color = defaultColor;
    }
    
    void Update()
    {
        // If aiming at object..
        if(interact.HasHit)
        {
            GameObject targetedObject = interact.CurrentObject;
            // If aiming at a pickup or interactable that's within reach...
        }
    }
}
