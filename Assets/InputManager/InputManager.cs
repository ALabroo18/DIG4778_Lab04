using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Creaete singleton reference for easy reference from other scripts.
    public static InputManager instance;

    // Reference to the player input component.
    public static PlayerInput playerInput;

    // Inputs that will be referenced in other scripts to get their values and use them.
    public Vector2 moveInput { get; private set; }
    public bool attackInput { get; private set; }
    public bool restartInput { get; private set; }

    // Input actions in the controls action map.
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction restartAction;

    private void Awake()
    {
        // Set singleton referene if it hasn't already been.
        if (instance == null)
        {
            instance = this;
        }

        // Assign playerInput reference.
        playerInput = GetComponent<PlayerInput>();

        // Setup the input actions so we can then use them.
        SetupInputActions();
    }

    private void Update()
    {
        // Constantly update the inputs to make the correct information is being sent to other scripts.
        UpdateInputs();
    }

    private void SetupInputActions()
    {
        // Assign the input action by indicating the action's name from the action map that is contained in the PlayerInput component.
        moveAction = playerInput.actions["Move"];
        attackAction = playerInput.actions["Attack"];
        restartAction = playerInput.actions["Restart"];
    }

    private void UpdateInputs()
    {
        // Set the values for the input actions.
        // Move uses vector 2 for x and y coords that are used to determine movement.
        moveInput = moveAction.ReadValue<Vector2>();

        // Attack action and restart action check if the user has pressed the button.
        attackInput = attackAction.WasPressedThisFrame();
        restartInput = restartAction.WasPressedThisFrame();
    }
}
