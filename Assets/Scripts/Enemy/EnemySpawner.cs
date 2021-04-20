using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float blueSparrowChance = 0.2f;
    [SerializeField]
    private float minSpawnDistance;
    [SerializeField]
    private float maxSpawnDistance;
    [SerializeField]
    private int maxEnemyCount = 10;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemyCount)
        {
            spawnNewEnemy();
        }
    }

    void spawnNewEnemy()
    {
        GameObject enemyPrefab = FindObjectOfType<GameManager>().GetPrefab("Enemy_GreenFalcon");
        
        if (Random.Range(0f, 1f) > 1f - this.blueSparrowChance)
        {
            enemyPrefab = FindObjectOfType<GameManager>().GetPrefab("Enemy_BlueSparrow");
        }
        
        var position = transform.position;

        while (Vector2.Distance(position, transform.position) < minSpawnDistance)
        {
            position = new Vector3(
            transform.position.x + Random.Range(-maxSpawnDistance, maxSpawnDistance),
            transform.position.y + Random.Range(-maxSpawnDistance, maxSpawnDistance),
            0);
        }
        Instantiate(enemyPrefab, position, Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f)));
    }
}
