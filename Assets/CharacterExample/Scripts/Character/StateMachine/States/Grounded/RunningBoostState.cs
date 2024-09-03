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

        View.StartRunningBoost();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopRunningBoost();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        Input.Movement.Boost.canceled += OnBoostCanceled;
        Input.Movement.Decrease.started += OnDecreaseStarted;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        Input.Movement.Boost.canceled -= OnBoostCanceled;
        Input.Movement.Decrease.started -= OnDecreaseStarted;
    }

    protected void OnBoostCanceled(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<RunningState>();

    protected void OnDecreaseStarted(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<WalkingState>();
}