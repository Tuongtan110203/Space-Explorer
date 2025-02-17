using UnityEngine;

public class CameraBounds : MonoBehaviour
{

    [Header("Map Collider")]
    public static CameraBounds instance;

    private Collider2D boundsCollider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        boundsCollider = GetComponent<Collider2D>();
    }

    public Bounds GetBounds()
    {
        if (boundsCollider != null)
        {
            return boundsCollider.bounds;
        }
        return new Bounds();
    }
}
