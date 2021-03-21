using UnityEngine;

public class BasicPowerController : MonoBehaviour
{
    [SerializeField]
    GameObject powerSource;
    [SerializeField]
    bool reversePower = false;

    void Awake()
    {
        if(powerSource)
        {
            powerSource.GetComponent<Power>().onPowerOn += Enable;
            powerSource.GetComponent<Power>().onPowerOff += Disable;
        }
        // If no powersource, always enabled.
    }

    void Enable()
    {
        gameObject.SetActive((!reversePower) ? true : false);
    }

    void Disable()
    {
        gameObject.SetActive((!reversePower) ? false : true);
    }
}
