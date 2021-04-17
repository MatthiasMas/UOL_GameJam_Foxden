using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody player;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float movementSpeed = 2f;    
    [SerializeField]
    private float boostSpeed = 4f;
    [SerializeField]
    private float boostLength = 0.2f;
    [SerializeField]
    private float timeBetweenBoosts = 2.5f;
    [SerializeField]
    private FogGeneration fog;

    private Vector2 lastPlayerPos = new Vector2(0f, 0f);
    private Vector2 deltaOffset = new Vector2(0f, 0f);
    private Vector3 input;
    private float speedMultiplier = 150f;
    private double lastBoost = 0;

    
    void Update()
    {
        this.input.x = Input.GetAxisRaw("Horizontal");
        this.input.y = Input.GetAxisRaw("Vertical");
        this.input.z = 0;

        this.animator.SetFloat("Horizontal", this.input.x);
        this.animator.SetFloat("Vertical", this.input.y);
        this.animator.SetFloat("Speed", this.input.sqrMagnitude);

        deltaOffset = lastPlayerPos - (Vector2)transform.position;

        lastPlayerPos = transform.position;
    }
    
    void FixedUpdate()
    {
        float speed = this.movementSpeed * this.speedMultiplier;
        double time = Time.timeAsDouble;
        
        if (Input.GetKey("space") && time > (this.lastBoost + this.boostLength + this.timeBetweenBoosts))
        {
            this.lastBoost = time;
        }

        if (time >= this.lastBoost && time <= (this.lastBoost + this.boostLength))
        {
            speed = this.boostSpeed * this.speedMultiplier;
        }

        Vector3 playerForce = this.input * (Time.fixedDeltaTime * speed);
        this.player.AddForce(playerForce);
    }

    public Vector2 getDeltaOffset()
    {
        return this.deltaOffset;
    }
}
