using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject GoonSpiderPrefab;
    private float spawnTimer;
    private float minSpawnTime = 5f; // minimum time to spawn object
    private float maxSpawnTime = 15f; // maximum time to spawn object

    private void Start()
    {
        spawnTimer = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }
    private void Update()
    {
        // check if it's time to spawn object
        if (Time.time >= spawnTimer)
        {
            spawnTimer = Time.time + Random.Range(minSpawnTime, maxSpawnTime); // set next spawn time
            SpawnEnemyPrefab();
        }
    }

    private void SpawnEnemyPrefab()
    {
        Instantiate(GoonSpiderPrefab, transform.position, Quaternion.identity).SetActive(true);
    }
}
