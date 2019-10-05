using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    // Modified from: https://learn.unity.com/tutorial/live-session-2d-platformer-character-controller

    public float maxSpeed = 7;
    public float jumpHeight = 2.0f;


    private SpriteRenderer spriteRenderer;
    private Animator animator;



    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    public bool godMode = false;

    LevelManager levelManager;

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(-2.0f * Physics2D.gravity.y * (jumpHeight + 0.5f));
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("isJumping", !grounded);
        animator.SetFloat("walkSpeed", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D: " + collision.gameObject.name);

        if (collision.GetComponent<DeathTrigger>() != null)
        {
            Die();
        }
        else if (collision.GetComponent<PickupBodyPart>() != null)
        {
            levelManager.UpdatePlayerTo(collision.GetComponent<PickupBodyPart>().NewPlayerPrefab);
            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        if (godMode)
        {
            Debug.Log("GOD MODE SAVE!");
            return;
        }

        Debug.Log("DIE!!!");
        // Spawn death animation

        Destroy(gameObject);


        levelManager.RestartLevel();
    }

}
