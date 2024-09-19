using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Create singleton for easy reference from other scripts.
    public static InputManager instance;

    public static PlayerInput playerInput;

    public Vector2 moveInput {  get; private set; }
    public bool attackInput { get; private set; }

    private InputAction moveAction;
    private InputAction attackAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    private void Update()
    {
        UpdateInputs();
    }

    private void SetupInputActions()
    {
        moveAction = playerInput.actions["Move"];
        attackAction = playerInput.actions["Attack"];
    }

    private void UpdateInputs()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        attackInput = attackAction.WasPressedThisFrame();
    }
}
