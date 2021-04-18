using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Transform playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        if (playerPosition == null)
        {
            return;
        }
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            FindObjectOfType<GameManager>().PlaySound("playerhit", GameManager.MixerGroup.SFX);
            player.TakeDamage();
        }
    }
}
