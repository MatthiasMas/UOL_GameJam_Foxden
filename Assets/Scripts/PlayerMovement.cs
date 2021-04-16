using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D player;
    public Animator animator;
    public float movementSpeed = 2f;
    private Vector2 input;
    
    void Update()
    {
        this.input.x = Input.GetAxisRaw("Horizontal");
        this.input.y = Input.GetAxisRaw("Vertical");

        this.animator.SetFloat("Horizontal", this.input.x);
        this.animator.SetFloat("Vertical", this.input.y);
        this.animator.SetFloat("Speed", this.input.sqrMagnitude);
    }
    
    void FixedUpdate()
    {
        this.player.MovePosition(this.player.position + (this.input * this.movementSpeed * Time.fixedDeltaTime));
    }
    
}
