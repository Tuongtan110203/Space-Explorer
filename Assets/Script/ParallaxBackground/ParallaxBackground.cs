using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;

    private float yPosition;
    private float height;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        height = GetComponent<SpriteRenderer>().bounds.size.y; // Lấy chiều cao của nền
        yPosition = transform.position.y;
    }

    void Update()
    {
        float distanceMoved = cam.transform.position.y * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.y * parallaxEffect;

        // Di chuyển theo trục Y
        transform.position = new Vector3(transform.position.x, yPosition + distanceToMove);

        // Kiểm tra nếu camera di chuyển quá xa, lặp lại nền
        if (distanceMoved > yPosition + height)
        {
            yPosition += height;
        }
        else if (distanceMoved < yPosition - height)
        {
            yPosition -= height;
        }
    }
}
