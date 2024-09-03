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

        View.StopRunning();
    }

    public override void Update( )
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        Input.Movement.Boost.started += OnBoostStarted;
        Input.Movement.Decrease.started += OnDecreaseStarted;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        Input.Movement.Boost.started -= OnBoostStarted;
        Input.Movement.Decrease.started -= OnDecreaseStarted;
    }

    protected void OnBoostStarted(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<RunningBoostState>();

    protected void OnDecreaseStarted(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<WalkingState>();
}