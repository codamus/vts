using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenConsole : MonoBehaviour
{
    public InputActionAsset inputActions;
    public GameObject canvasObject;

    private InputAction menu;
  
    //find action in action map default from xr interaction toolkit
    private void Start()
    {
        menu = inputActions.FindActionMap("XRI LeftHand Interaction").FindAction("Open Console");
        menu.Enable();
        menu.performed += ToggleCanvas();
    }

    private Action<InputAction.CallbackContext> ToggleCanvas()
    {
        canvasObject.SetActive(!canvasObject.activeSelf);
        return null;
    }


}
