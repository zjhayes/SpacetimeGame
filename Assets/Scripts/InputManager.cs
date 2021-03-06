using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Singleton
        public static InputManager instance;

        void Awake() 
        {
            instance = this;
            if(controls == null) { controls = new PlayerControls(); }
        }
    #endregion

    private PlayerControls controls;

    public PlayerControls Controls
    {
        get { return controls; }
    }

    void OnEnable()
    {
        // Turn controls on with this object.
        controls.Enable();
    }

    void OnDisable()
    {
        // Turn controls off with this object.
        controls.Disable();
    }
}
