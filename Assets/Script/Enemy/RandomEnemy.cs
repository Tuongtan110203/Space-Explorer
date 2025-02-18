using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform pointA, pointB;
    public float spawnInterval = 0.2f;

    void Start()
    {
        InvokeRepeating("SpawnMeteorite", 1f, spawnInterval);
        InvokeRepeating("SpawnStar", 10f, 20f);
    }

    void SpawnMeteorite()
    {
        if (prefabs.Length == 0 || pointA == null || pointB == null) return;

        int randomIndex = Random.Range(0, prefabs.Length);
        if (randomIndex == 2) return;

        SpawnPrefabs(randomIndex);
    }

    void SpawnStar()
    {
        if (prefabs.Length > 2)
        {
            SpawnPrefabs(2); 
        }
    }

    void SpawnPrefabs(int index)
    {
        float spawnX = Random.Range(pointA.position.x, pointB.position.x);
        float cameraTopY = Camera.main.transform.position.y + Camera.main.orthographicSize;
        float spawnY = cameraTopY + 0.5f;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject spawnedObject = Instantiate(prefabs[index], spawnPosition, Quaternion.identity);

        float elapsedTime = Time.timeSinceLevelLoad;

        if (elapsedTime > 30f)
        {
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale *= 1.5f;
            }
        }

        Destroy(spawnedObject, 2f);
    }
}
