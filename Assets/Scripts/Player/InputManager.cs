using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private Inputs inputs;

    private void Awake()
    {
        inputs = new Inputs();

        inputs.Player.Run.started += ctx => StartRun(ctx);
        inputs.Player.Run.canceled += ctx => EndRun(ctx);

        inputs.Player.Light.started += ctx => StartLight(ctx);

        inputs.Player.Pause.started += ctx => StartOption(ctx);

        inputs.Player.Interacte.started += ctx => StartInteracte(ctx);
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    public Vector2 GetMouseDelta()
    {
        return inputs.Player.Look.ReadValue<Vector2>();
    }
    public Vector2 GetPlayerMovement()
    {
        return inputs.Player.Move.ReadValue<Vector2>();
    }

    public delegate void StartRunEvent();
    public event StartRunEvent OnStartRun;
    private void StartRun(InputAction.CallbackContext context) 
        { if (OnStartRun != null) OnStartRun(); }
    public delegate void EndRunEvent();
    public event EndRunEvent OnEndRun;
    private void EndRun(InputAction.CallbackContext context)
    { if (OnEndRun != null) OnEndRun(); }
    public bool DetectRun()
    {
        return inputs.Player.Run.ReadValue<bool>();
    }

    public delegate void StartLightEvent();
    public event StartLightEvent OnStartLight;
    private void StartLight(InputAction.CallbackContext context)
    { if (OnStartLight != null) OnStartLight(); }


    public delegate void StartOptionEvent();
    public event StartOptionEvent OnStartOption;
    private void StartOption(InputAction.CallbackContext context)
    { if (OnStartOption != null) OnStartOption(); }

    public delegate void StartInteracteEvent();
    public event StartInteracteEvent OnStartInteracte;
    private void StartInteracte(InputAction.CallbackContext context)
    { if (OnStartInteracte != null) OnStartInteracte(); }
}
