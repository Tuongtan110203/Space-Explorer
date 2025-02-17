using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;
    private SoundManager soundManager;
    private void Awake()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(1); 
            }
            else
            {
                Debug.LogError("ScoreManager instance is NULL! Kiểm tra lại trong Scene.");
            }
            if (explosionPrefab != null)
            {

                GameObject explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
                Destroy(explosion, 0.5f); 
            }
            soundManager.PlayHitAsteroidSound();
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
        }
    }
}
