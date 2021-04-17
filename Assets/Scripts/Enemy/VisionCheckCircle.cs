using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCheckCircle : MonoBehaviour
{

    public EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyMovement.isInFOV = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyMovement.isInFOV = false;
        }
    }
}
