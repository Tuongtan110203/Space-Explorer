using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform pointA, pointB; 
    public float spawnInterval = 0.5f;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", 1f, spawnInterval);
    }

    void SpawnEnemies()
    {
        if (prefabs.Length == 0 || pointA == null || pointB == null) return;

        float spawnX = Random.Range(pointA.position.x, pointB.position.x);

        float cameraTopY = Camera.main.transform.position.y + Camera.main.orthographicSize;

        float spawnY = cameraTopY + .5f;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        // Spawn enemy
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject selectedPrefab = prefabs[randomIndex];
        Debug.Log("Prefab được chọn: " + selectedPrefab.name);
        GameObject spawnedObject = Instantiate(prefabs[randomIndex], spawnPosition, Quaternion.identity);
       
        Destroy(spawnedObject, 2f);
    }
}
