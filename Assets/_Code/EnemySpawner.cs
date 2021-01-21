using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] EnemyController prefab;
    [SerializeField] float spawnInterval = 5;
    [SerializeField] Transform[] spawnPoints;

    float spawnDelayTimer = 3;
    

    void Update() {
        if (spawnDelayTimer > 0) {
            spawnDelayTimer -= Time.deltaTime;
            return;
        }
        var allEnemies = FindObjectsOfType<EnemyController>();
        if(allEnemies.Length >= 2) { 
            spawnDelayTimer = 1;
            return;
        }
        spawnDelayTimer = spawnInterval;
        var spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(prefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
    }
}