using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                player.TakeDame(damage);
            }
        }
    }
}
