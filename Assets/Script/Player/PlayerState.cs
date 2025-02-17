using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;


    protected float xInput;
    protected float yInput;
    private string animBoolName;
    private float stateTimer;
    //protected bool triggerCalled;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        //stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
    }
}
