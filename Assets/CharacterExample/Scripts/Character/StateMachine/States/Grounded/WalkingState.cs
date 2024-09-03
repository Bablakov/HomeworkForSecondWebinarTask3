using UnityEngine.InputSystem;
using UnityEngine;

public class WalkingState : GroundedMoveState
{
    private RunningStateConfig _config;

    public WalkingState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        => _config = character.Config.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _config.WalkingSpeed;

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
        => Debug.Log("OnBoostCanceled in walkingState");
    protected override void OnBoostStarted(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<RunningBoostState>();

    protected override void OnDecreaseCanceled(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<RunningState>();

    protected override void OnDecreaseStarted(InputAction.CallbackContext obj)
        => Debug.Log("OnDecreseStarted in walkingState");
}