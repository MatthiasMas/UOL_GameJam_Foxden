using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    [SerializeField]
    private DebrisPart[] debrisParts;
    private Transform playerTransform;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            return;
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        foreach (DebrisPart part in debrisParts)
        {
            GameObject[] partObjects = GameObject.FindGameObjectsWithTag(part.name);
            float minDistance = Mathf.Infinity;
            foreach (GameObject o in partObjects)
            {
                if (Vector2.Distance(o.transform.position, playerTransform.position) < minDistance)
                {
                    minDistance = Vector2.Distance(o.transform.position, playerTransform.position);
                }
            }

            if (minDistance > part.distanceBeforeNextSpawn)
            {
                GenerateNewDebris(part);
            }
        }
    }

    void GenerateNewDebris(DebrisPart part)
    {
        GameObject debrisPrefab = FindObjectOfType<GameManager>().GetPrefab(part.partName);
        var position = transform.position;

        while (Vector2.Distance(position, transform.position) < part.minSpawnDistance)
        {
            position = new Vector3(
            transform.position.x + Random.Range(-part.maxSpawnDistance, part.maxSpawnDistance),
            transform.position.y + Random.Range(-part.maxSpawnDistance, part.maxSpawnDistance),
            0);
        }
        Instantiate(debrisPrefab, position, Quaternion.identity);
    }
}
