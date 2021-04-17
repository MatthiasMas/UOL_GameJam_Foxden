using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float moveSpeed;

    private Transform playerPosition;

    [SerializeField]
    private Vector2 goalPosition;
    [SerializeField]
    private float goalReachedDistance;
    [SerializeField]
    private float newGoalDistance;

    public bool isInFOV;
    private bool wasInFOV;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateNewGoalPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir;

        if (wasInFOV && !isInFOV)                                                               // enemy has seen the player BEFORE but should generate a new random pos now
        {
            GenerateNewGoalPosition();
        }
        if (isInFOV)                                                                            // the enemy has seen the player and should move towards him
        {
            wasInFOV = true;
            dir = (playerPosition.position - transform.position).normalized;
        }
        else
        {
            wasInFOV = false;
            dir = (goalPosition - (Vector2)transform.position).normalized;
            if (Vector2.Distance(transform.position, goalPosition) <= goalReachedDistance)      // enemy reached its current goal and should generate a new one
            {
                GenerateNewGoalPosition();
            }
        }

        Move(dir);
        RotateObject(dir);
    }

    private void Move(Vector2 dir)
    {
        Vector2 newPos = new Vector2(
            transform.position.x + dir.x * moveSpeed, 
            transform.position.y + dir.y * moveSpeed);
        transform.position = newPos;
    }

    private void GenerateNewGoalPosition()
    {
        goalPosition = new Vector2(
            transform.position.x + Random.Range(-newGoalDistance, newGoalDistance),
            transform.position.y + Random.Range(-newGoalDistance, newGoalDistance));
    }

    void RotateObject(Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 400 * Time.deltaTime);
    }
}