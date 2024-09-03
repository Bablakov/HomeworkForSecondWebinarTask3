using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunningState : GroundedMoveState
{
    private RunningStateConfig _config;

    public RunningState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        => _config = character.Config.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _config.RunningSpeed;

        View.StartRunning();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update( )
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
    }

    protected override void OnBoostCanceled(InputAction.CallbackContext obj)
        => Debug.Log("OnBoostCanceled in runningState");
    protected override void OnBoostStarted(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<RunningBoostState>();

    protected override void OnDecreaseCanceled(InputAction.CallbackContext obj)
        => Debug.Log("OnDecreseCanceled in runningState");

    protected override void OnDecreaseStarted(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<WalkingState>();
}