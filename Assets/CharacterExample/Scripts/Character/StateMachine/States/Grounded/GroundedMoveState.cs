using UnityEngine.InputSystem;

public abstract class GroundedMoveState : GroundedState
{
    public GroundedMoveState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
    { }

    public override void Enter()
    {
        base.Enter();

        Subscribe();

        StateSwitcher.SwitchState<RunningState>();

        View.StartRunning();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();

        if (Input.Movement.Boost.ReadValue<bool>())
            StateSwitcher.SwitchState<RunningBoostState>();

        if (Input.Movement.Decrease.ReadValue<bool>())
            StateSwitcher.SwitchState<WalkingState>();
    }

    protected override void Subscribe()
    {
        Input.Movement.Boost.started += OnBoostStarted;
        Input.Movement.Boost.canceled += OnBoostCanceled;
        Input.Movement.Decrease.started += OnDecreaseStarted;
        Input.Movement.Decrease.canceled += OnDecreaseCanceled;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();
        Input.Movement.Boost.started -= OnBoostStarted;
        Input.Movement.Boost.canceled -= OnBoostCanceled;
        Input.Movement.Decrease.started -= OnDecreaseStarted;
        Input.Movement.Decrease.canceled -= OnDecreaseCanceled;
    }

    protected abstract void OnBoostStarted(InputAction.CallbackContext obj);

    protected abstract void OnBoostCanceled(InputAction.CallbackContext obj);

    protected abstract void OnDecreaseStarted(InputAction.CallbackContext obj);

    protected abstract void OnDecreaseCanceled(InputAction.CallbackContext obj);
}