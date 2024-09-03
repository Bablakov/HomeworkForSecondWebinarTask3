using UnityEngine.InputSystem;

public class WalkingState : GroundedMoveState
{
    private RunningStateConfig _config;

    public WalkingState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        => _config = character.Config.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _config.WalkingSpeed;

        View.StartWalking();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopWalking();
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

        Input.Movement.Boost.started += OnBoostStarted;
        Input.Movement.Decrease.canceled += OnDecreaseCanceled;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        Input.Movement.Boost.started -= OnBoostStarted;
        Input.Movement.Decrease.canceled -= OnDecreaseCanceled;
    }

    protected void OnBoostStarted(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<RunningBoostState>();

    protected void OnDecreaseCanceled(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<RunningState>();
}