using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float baseSpeed;
    private float moveSpeed;
    private float speedMultiplier;

    private Transform playerPosition;

    [SerializeField]
    private Vector2 goalPosition;
    [SerializeField]
    private float goalReachedDistance;
    [SerializeField]
    private float newGoalDistance;
    [SerializeField]
    private float rotationSpeed;

    public bool isInFOV;
    public bool wasInFOV;

    private float updateTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateNewGoalPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerPosition == null)
        {
            return;
        }

        speedMultiplier = 1f + 0.1f * playerPosition.gameObject.GetComponent<Player>().getInventory().count();
        moveSpeed = baseSpeed * speedMultiplier;

        updateTimer -= Time.deltaTime;
        if (updateTimer < 0f)
        {
            updateTimer = 2f;
            UpdateGoalPosition();
        }

        Vector2 dir = Vector2.zero;

        if (isInFOV)                                                                            // the enemy has seen the player and should move towards him
        {
            dir = (playerPosition.position - transform.position).normalized;
        }
        else
        {
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

    private void UpdateGoalPosition()
    {
        goalPosition.x += Random.Range(-4f, 4f);
        goalPosition.y += Random.Range(-4f, 4f);
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
        if (isInFOV)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 100 * Time.fixedDeltaTime);
        }
        
    }
}
