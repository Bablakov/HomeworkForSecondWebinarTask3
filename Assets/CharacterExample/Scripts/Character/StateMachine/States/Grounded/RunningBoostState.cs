using UnityEngine;
using UnityEngine.InputSystem;

public class RunningBoostState : GroundedMoveState
{
    private RunningStateConfig _config;

    public RunningBoostState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        => _config = character.Config.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _config.RunningBoostSpeed;

        View.StartRunning();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopRunning();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
    }

    protected override void OnBoostCanceled(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<RunningState>();
    protected override void OnBoostStarted(InputAction.CallbackContext obj)
        => Debug.Log("OnBoostStarted in runningBoostState");

    protected override void OnDecreaseCanceled(InputAction.CallbackContext obj)
        => Debug.Log("OnDecreseCanceled in runningBoostState");

    protected override void OnDecreaseStarted(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<WalkingState>();
}