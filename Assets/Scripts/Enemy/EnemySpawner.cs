using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float minSpawnDistance;
    public float maxSpawnDistance;

    [SerializeField]
    private int maxEnemyCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemyCount)
        {
            spawnNewEnemy();
        }
    }

    void spawnNewEnemy()
    {
        GameObject enemyPrefab = FindObjectOfType<GameManager>().GetPrefab("Enemy");
        var position = transform.position;

        while (Vector2.Distance(position, transform.position) < minSpawnDistance)
        {
            position = new Vector3(
            Random.Range(-maxSpawnDistance, maxSpawnDistance),
            Random.Range(-maxSpawnDistance, maxSpawnDistance),
            0);
        }
        Instantiate(enemyPrefab, position, Quaternion.identity);
        // TODO: ENEMY SPAWN SOUND
    }
}
