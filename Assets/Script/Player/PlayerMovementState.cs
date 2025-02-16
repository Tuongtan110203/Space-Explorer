using UnityEngine;

public class PlayerMovementState : PlayerState
{
    public PlayerMovementState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0 || yInput != 0)
        {
            Vector2 inputDirection = new Vector2(xInput, yInput).normalized;
            player.SetVelocity(inputDirection.x * (player.moveSpeed / 2), inputDirection.y * player.moveSpeed * 2);
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }

    }
}
