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

    public float enemyTakeDamageAmount;
    public float enemySpeedAmount;

    public GameController controller;
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
        var enemy = Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(-randomizePosOnX,randomizePosOnX),0,0),
            transform.rotation);
        enemy.GetComponent<Enemy>().takeDamageAmount = enemyTakeDamageAmount;
        enemy.GetComponent<Enemy>().speed = enemySpeedAmount;
        enemy.GetComponent<Enemy>().controller = controller;
    }
}
