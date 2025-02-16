using UnityEngine;

public class Star : MonoBehaviour
{
    private ScoreManager scoreManager;
    private SoundManager soundManager;
    private void Awake()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        soundManager = FindAnyObjectByType<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            Destroy(collision.gameObject);
            soundManager.PlayStarSound();
            scoreManager.AddScore(1);
        }
    }
}
