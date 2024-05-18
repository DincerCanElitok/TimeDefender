using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTimeMax;
    public float spawnTimeMin;
    public float spawnTime;
    private float currentTime = 0f;

    public float randomizePosOnX;

    private void Start()
    {
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= spawnTime)
        {
            SpawnEnemy();
            currentTime = 0f;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(-randomizePosOnX,randomizePosOnX),0,0),
            transform.rotation);
    }
}
