using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed;

    [Header("Shooting Info")]
    public GameObject bulletPrefab;
    public Transform firePoint;     
    public float bulletSpeed = 25f; 
    public float fireRate = 0.5f;



    #region component
    public Rigidbody2D rb;
    public Animator animator { get; private set; }

    #endregion

    #region States 
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerMovementState movementState { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerShootingState shootingState { get; private set; } 
    #endregion states



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();


        stateMachine = new PlayerStateMachine();
        movementState = new PlayerMovementState(this, stateMachine, "Move");
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        shootingState = new PlayerShootingState(this, stateMachine, "Shoot"); 

    }

    private void Start()
    {
        stateMachine.Initialize(idleState);

       
    }

    private void Update()
    {
        stateMachine.currentState.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(shootingState);
        }
    }

    public virtual void SetZeroVelocity()
    {

        rb.linearVelocity = Vector2.zero;

    }
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity );
    }

    public virtual void DestroyObject(GameObject obj, float delay)
    {
        Destroy(obj, delay);
    }

    public virtual void TakeDame()
    {

    }
}
