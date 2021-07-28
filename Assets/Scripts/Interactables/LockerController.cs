using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Attach controller to locker door. */
[RequireComponent(typeof(Interactable))]
public class LockerController : MonoBehaviour
{
    [SerializeField]
    GameObject hidePosition;
    [SerializeField]
    GameObject exitPosition;
    [SerializeField]
    float hideSpeed = 5.0f;
    [SerializeField]
    float doorSpeed = 100.0f;
    [SerializeField]
    float maxDoorRotation = 90.0f;
    [SerializeField]
    Tooltip tooltip;

    Interactable interaction;
    bool isHiding = false;

    void Start()
    {
        interaction = gameObject.GetComponent<Interactable>();
        interaction.onInteract += Interact;
        tooltip.TooltipText = "Hide";
    }

    void Interact()
    {
        if(isHiding)
        {
            isHiding = false;
            StartCoroutine(Exit());
        }
        else
        {
            isHiding = true;
            StartCoroutine(Enter());
        }
    }

    IEnumerator Enter()
    {
        // Pause player control, open door, player enters locker, close door.
        PausePlayerControls(true);
        yield return StartCoroutine(UpdateDoorRotation(maxDoorRotation));
        yield return StartCoroutine(UpdatePlayerPosition(hidePosition.transform));
        tooltip.TooltipText = "Exit";
        yield return StartCoroutine(UpdateDoorRotation(0));
    }

    IEnumerator Exit()
    {
        // Open door, player exits locker, close door, restore player control.
        yield return StartCoroutine(UpdateDoorRotation(maxDoorRotation));
        yield return StartCoroutine(UpdatePlayerPosition(exitPosition.transform));
        tooltip.TooltipText = "Hide";
        yield return StartCoroutine(UpdateDoorRotation(0));
        PausePlayerControls(false);
    }

    IEnumerator UpdateDoorRotation(float angle)
    {
        Quaternion doorRotation = Quaternion.Euler(0, angle, 0);
        // Gradually updates door rotation.
        while(transform.localEulerAngles.y != angle)
        {
            float step = doorSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, doorRotation, step);
            yield return null;
        }
    }

    IEnumerator UpdatePlayerPosition(Transform target)
    {
        while(PlayerManager.instance.Player.transform.position != target.position &&
            PlayerManager.instance.Player.transform.rotation != target.rotation)
        {
            // Move player to target, rotate to look forward.
            float step = hideSpeed * Time.deltaTime;
            PlayerManager.instance.Player.transform.position = Vector3.MoveTowards(PlayerManager.instance.Player.transform.position, target.position, step);
            PlayerManager.instance.Player.transform.rotation = Quaternion.Slerp(PlayerManager.instance.Player.transform.rotation, target.rotation, step);
            yield return null;
        }
    }

    void PausePlayerControls(bool pause)
    {
        // Prevent player from moving while in locker.
        PlayerManager.instance.Player.GetComponent<PlayerMovement>().CanMove = !pause;
    }
}
