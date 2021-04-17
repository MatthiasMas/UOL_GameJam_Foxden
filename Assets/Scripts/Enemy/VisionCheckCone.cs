using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCheckCone : MonoBehaviour
{

    public EnemyMovement enemyMovement;
    public EnemyAttack enemyAttack;

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
            enemyMovement.isInFOV = true;
            enemyAttack.canAttack = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyMovement.isInFOV = false;
            enemyAttack.canAttack = false;
        }
    }
}
