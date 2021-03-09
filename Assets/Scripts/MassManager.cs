using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassManager : MonoBehaviour
{
    #region Singleton
        public static MassManager instance;

        void Awake() 
        {
            if(instance != null)
            {
                Debug.LogWarning("More than one instance of MassManager found.");
                return;
            }
            instance = this;
        }
    #endregion

    [SerializeField]
    private GameObject centerOfMass;

    public GameObject CenterOfMass
    {
        get { return centerOfMass; }
        set 
        {
            if(value.GetComponent<CenterOfMass>())
            {
                centerOfMass = value;
            }
        }
    }
}
