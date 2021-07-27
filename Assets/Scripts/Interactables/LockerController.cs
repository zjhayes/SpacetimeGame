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
    float hideSpeed = 5.0f;
    [SerializeField]
    float doorSpeed = 100.0f;
    [SerializeField]
    float maxDoorRotation = 90.0f;

    Interactable interaction;
    bool isHiding = false;
    bool doorOpen = false;

    void Start()
    {
        interaction = gameObject.GetComponent<Interactable>();
        interaction.onInteract += Interact;
    }

    void Interact()
    {
        if(isHiding)
        {
            isHiding = false;
            PausePlayerControls(false);
            StartCoroutine(Exit());
        }
        else
        {
            isHiding = true;
            PausePlayerControls(true);
            StartCoroutine(Enter());
        }
    }

    IEnumerator Enter()
    {
        yield return StartCoroutine(UpdateDoorRotation(maxDoorRotation));
        yield return StartCoroutine(UpdatePlayerPosition(hidePosition.transform));
        yield return StartCoroutine(UpdateDoorRotation(0));
    }

    IEnumerator Exit()
    {
        yield return StartCoroutine(UpdateDoorRotation(maxDoorRotation));
        yield return StartCoroutine(UpdateDoorRotation(0));
    }

    IEnumerator UpdateDoorRotation(float angle)
    {
        Quaternion doorRotation = Quaternion.Euler(0, angle, 0);
        // Gradually updates door rotation.
        while(transform.eulerAngles.y != angle)
        {
            float step = doorSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, doorRotation, step);
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

    IEnumerator PlayerEnter()
    {
        while(PlayerManager.instance.Player.transform.position != hidePosition.transform.position &&
            PlayerManager.instance.Player.transform.rotation != hidePosition.transform.rotation)
        {
            // Move player to locker, rotate to look forward.
            float step = hideSpeed * Time.deltaTime;
            PlayerManager.instance.Player.transform.position = Vector3.MoveTowards(PlayerManager.instance.Player.transform.position, hidePosition.transform.position, step);
            PlayerManager.instance.Player.transform.rotation = Quaternion.Slerp(PlayerManager.instance.Player.transform.rotation, hidePosition.transform.rotation, step);
            yield return null;
        }
    }

    void PausePlayerControls(bool pause)
    {
        PlayerManager.instance.Player.GetComponent<PlayerMovement>().CanMove = !pause;
    }
}
