using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    // Modified from: https://learn.unity.com/tutorial/live-session-2d-platformer-character-controller

    public float maxSpeed = 7;
    public float jumpHeight = 2.0f;
    float flinchSpeed = 2;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Health health;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        health = GetComponentInChildren<Health>();
    }

    public bool godMode = false;

    LevelManager levelManager;

    public bool isLoading;

    protected override void ComputeVelocity()
    {
        if (isLoading)
        {
            targetVelocity = Vector2.zero;
            return;
        }

        Vector2 move = Vector2.zero;


        if (health.IsFlinching())
        {
            move.x = (spriteRenderer.flipX) ? flinchSpeed : -flinchSpeed;
        }
        else
        {
            move.x = Input.GetAxis("Horizontal");
        }

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

        if (health.IsFlinching() == false)
        {
            bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
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
            GetComponent<Health>().Die();
        }
        else if (collision.GetComponent<PickupBodyPart>() != null)
        {
            levelManager.UpdatePlayerTo(collision.GetComponent<PickupBodyPart>().NewPlayerPrefab);
            Destroy(collision.gameObject);
        }
        else if (collision.GetComponent<LevelExit>() != null)
        {
            isLoading = true;
            levelManager.FinishLevel();
        }
    }

    private void OnDestroy()
    {
        levelManager.RestartLevel();
    }

}
