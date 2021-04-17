using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody rb;
    private bool hasHit = false;

    Player player;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartProjectile(Vector3 direction, float projectileSpeed)
    {
        rb.velocity = direction * projectileSpeed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "ShieldEquipped" && !hasHit)
        {
            hasHit = true;
            // TODO: SOUND SHIELD HIT
            player.HitShield(collision.gameObject.name);
            Destroy(gameObject);
        }

        if (collision.tag == "Player" && !hasHit)
        {
            hasHit = true;
            // TODO: SOUND PLAYER HIT
            player.TakeDamage();
            Destroy(gameObject);
        }

        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
