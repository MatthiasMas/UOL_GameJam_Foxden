using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCheck : MonoBehaviour
{

    public EnemyMovement enemy;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print(gameObject.name + " collided with player");
            enemy.isInFOV = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print(gameObject.name + "stop colliding with player");
            enemy.isInFOV = false;
        }
    }
}
