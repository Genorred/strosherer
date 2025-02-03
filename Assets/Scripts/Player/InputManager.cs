using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Userinput userinput;
    public Userinput.OnFootActions onFootActions;
    private PlayerLook playerLook;
    private PlayerMotor motor;
    private Vector2 cachedMovementInput;
    private Vector2 cachedLookInput;

    private void Awake()
    {
        userinput = new Userinput();
        onFootActions = userinput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        onFootActions.Jump.performed += ctx => motor.JumpHandler();
        onFootActions.Dash.performed += ctx => motor.Dash();
    }

    private void FixedUpdate()
    {
        motor.ProcessMove(cachedMovementInput);
    }

    private void Update()
    {
        playerLook.ProcessLook(cachedLookInput);
    }

    private void LateUpdate()
    {
        
    }

    private void OnEnable()
    {
        onFootActions.Enable();

        onFootActions.Movement.performed += OnMovementPerformed;
        onFootActions.Movement.canceled += OnMovementCanceled;
        onFootActions.Look.performed += OnLookPerformed;
        onFootActions.Look.canceled += OnLookCanceled;
    }
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        cachedMovementInput = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        cachedMovementInput = Vector2.zero;
    }
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        cachedLookInput = context.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        cachedLookInput = Vector2.zero;
    }
    private void OnDisable()
    {
        onFootActions.Disable();
    }
}