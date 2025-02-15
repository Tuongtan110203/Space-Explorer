using UnityEngine;

public class PlayerShootingState : PlayerState
{
    private float nextFireTime;


    public PlayerShootingState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        nextFireTime = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + player.fireRate; 
            
        }

        if (!Input.GetKey(KeyCode.Space))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    private void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(player.bulletPrefab, player.firePoint.position, Quaternion.identity);
        
        // xet lai parent k no hut theo may bay
        bullet.transform.parent = null;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, player.bulletSpeed + player.rb.linearVelocity.y);
        }


        PlayerManager.instance.player.DestroyObject(bullet, .8f);
    }
}
