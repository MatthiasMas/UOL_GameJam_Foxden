using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject initiator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody rb;
    private bool hasHit = false;

    Player player;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartProjectile(GameObject initiator, Vector3 direction, float projectileSpeed)
    {
        this.initiator = initiator;
        rb.velocity = direction * projectileSpeed;

        if (Random.Range(0f, 1f) > 0.98f)
        {
            spriteRenderer.sprite = FindObjectOfType<GameManager>().GetSprite("TX Village Props_1");
            FindObjectOfType<GameManager>().PlaySound("arrow", GameManager.MixerGroup.SFX);
            return;
        }

        if (initiator.tag == "Enemy")
        {
            float random = Random.Range(0f, 1f);
            if (random < 0.3f)
            {
                FindObjectOfType<GameManager>().PlaySound("blaster2", GameManager.MixerGroup.SFX);
                return;
            }
            if (random < 0.7f)
            {
                FindObjectOfType<GameManager>().PlaySound("blaster3", GameManager.MixerGroup.SFX);
                return;
            }
            if (random >= 0.7f)
            {
                FindObjectOfType<GameManager>().PlaySound("blaster4", GameManager.MixerGroup.SFX);
                return;
            }
        }
        if (initiator.tag == "Player")
        {
            FindObjectOfType<GameManager>().PlaySound("blaster1", GameManager.MixerGroup.SFX);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (initiator == null)
        {
            Destroy(gameObject);
        }

        if (initiator.tag == "Enemy")
        {
            if (collision.tag == "ShieldEquipped" && !hasHit)
            {
                hasHit = true;
                FindObjectOfType<GameManager>().PlaySound("shieldhit", GameManager.MixerGroup.SFX);
                player.HitShield(collision.gameObject.name);
                Destroy(gameObject);
            }

            if (collision.tag == "Player" && !hasHit)
            {
                hasHit = true;
                FindObjectOfType<GameManager>().PlaySound("playerhit", GameManager.MixerGroup.SFX);
                player.TakeDamage();
                Destroy(gameObject);
            }
        }
        if (initiator.tag == "Player")
        {
            if (collision.tag == "Enemy")
            {
                FindObjectOfType<GameManager>().PlaySound("playerhit", GameManager.MixerGroup.SFX);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        

        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
