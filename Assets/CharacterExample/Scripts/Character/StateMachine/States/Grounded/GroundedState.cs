using UnityEngine.InputSystem;

public abstract class GroundedState : MovementState
{
    private GroundChecker _groundChecker;

    public GroundedState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        => _groundChecker = character.GroundChecker;

    public override void Enter()
    {
        base.Enter();

        Subscribe();

        View.StartGrounded();
    }


    public override void Exit()
    {
        base.Exit();

        Unsubscribe();

        View.StopGrounded();
    }

    public override void Update()
    {
        base.Update();

        if (_groundChecker.IsTouches == false)
            StateSwitcher.SwitchState<FallingState>();
    }

    protected virtual void Subscribe()
    {
        Input.Movement.Jump.started += OnJumpKeyPressed;
    }

    protected virtual void Unsubscribe()
    {
        Input.Movement.Jump.started -= OnJumpKeyPressed;
    }

    private void OnJumpKeyPressed(InputAction.CallbackContext obj)
        => StateSwitcher.SwitchState<JumpingState>();
}
