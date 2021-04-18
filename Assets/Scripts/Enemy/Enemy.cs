using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Transform playerPosition;
    [SerializeField]
    private float despawnDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition == null)
        {
            return;
        }

        if (Vector2.Distance(transform.position, playerPosition.position) >= despawnDistance)
        {
            Destroy(gameObject);
        }
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
