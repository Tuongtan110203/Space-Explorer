using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed;

    [Header("Shooting Info")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 25f;
    public float fireRate = 0.5f;

    [Header("Health")]
    public float startingHealth;

    [Header("IsFrames")]
    public float iFrameDuration;
    public int numberOfFlash;

    [Header("Injury Sound")]
    public AudioClip deathSound;
    public AudioClip hurtSound;

    [Header("Menu + Restart")]
    public MenuController gameMenu;
    public HealthBar healthBar;
    public RandomEnemy randomEnemy;
    public ScoreManager scoreManager;
    

    #region Map(Min Max)
    private float minX, maxX, minY, maxY;
    #endregion

    #region DIE HURT
    public float currentHealth { get; private set; }
    private bool invulnerable;
    #endregion

    #region component
    public Rigidbody2D rb;
    public Animator animator { get; private set; }

    private SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameMenu.PauseGame();
        }

        UpdateBounds();

        stateMachine.currentState.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(shootingState);
        }
    }

    void UpdateBounds()
    {
        if (CameraBounds.instance != null)
        {
            Bounds bounds = CameraBounds.instance.GetBounds();
            minX = bounds.min.x;
            maxX = bounds.max.x;
            minY = bounds.min.y;
            maxY = bounds.max.y;
        }
    }

    public virtual void SetZeroVelocity()
    {

        rb.linearVelocity = Vector2.zero;

    }
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        //rb.linearVelocity = new Vector2(_xVelocity, _yVelocity );

        Vector2 newPosition = rb.position + new Vector2(_xVelocity, _yVelocity) * Time.fixedDeltaTime;

        // Giới hạn vị trí theo CameraBounds
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        rb.linearVelocity = (newPosition - rb.position) / Time.fixedDeltaTime;
    }

    public virtual void DestroyObject(GameObject obj, float delay)
    {
        Destroy(obj, delay);
    }

    public void AddHealth(float _health)
    {
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, startingHealth);
    }

    public virtual void TakeDame(float _damage)
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            if (invulnerable)
            {
                return;
            }

            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

            if (currentHealth > 0)
            {
                ScoreManager.instance.DecreaseScore(1);
                StartCoroutine(Invunerability());
                SoundManager.instance.PlaySound(hurtSound);
            }
            else
            {
                animator.SetTrigger("Die");
                SoundManager.instance.PlaySound(deathSound);
                gameObject.SetActive(false);
                gameMenu.ShowScoreResult(); 
            }
        }
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);

        for (int i = 0; i < numberOfFlash; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
        invulnerable = false;
    }

    public void RestartGame()
    {
        stateMachine.Initialize(idleState);

        #region Health
        currentHealth = startingHealth;
        healthBar.UpdateHealthBar();
        #endregion

        #region Enemy Spawn

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.SetActive(false);
        }

        randomEnemy.CancelInvoke("SpawnEnemies");
        randomEnemy.InvokeRepeating("SpawnEnemies", 1f, randomEnemy.spawnInterval);
        #endregion

        #region Score
        ScoreManager.instance.ResetScore();
        #endregion

        gameObject.SetActive(true);
    }

    private void Deactive() //attach to the end of the exploe anim
    {
        gameObject.SetActive(false);
    }

}
