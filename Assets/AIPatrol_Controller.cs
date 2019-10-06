using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol_Controller : PhysicsObject
{
    // Modified from: https://learn.unity.com/tutorial/live-session-2d-platformer-character-controller

    public float maxSpeed = 7;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider;

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if (IsSafeToAdvance() == false)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }


        move = new Vector2(
            (spriteRenderer.flipX ? -1 : 1),
            0);


        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("isJumping", !grounded);
        animator.SetFloat("walkSpeed", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    bool IsSafeToAdvance()
    {
        return HasSafeGround() && HasSpaceAhead();
    }

    bool HasSpaceAhead()
    {
        Vector3 rayPos = new Vector3();

        rayPos.y = boxCollider.bounds.center.y;

        if (spriteRenderer.flipX == false)  // going right
        {
            rayPos.x = boxCollider.bounds.max.x;
        }
        else
        {
            rayPos.x = boxCollider.bounds.min.x;
        }

        RaycastHit2D[] hits = Physics2D.BoxCastAll(rayPos, boxCollider.bounds.size, 0f, 
            (spriteRenderer.flipX) ? Vector2.left : Vector2.right,
            0.1f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == boxCollider)
            {
                // stop hitting yourself
                continue;
            }

            if (hit.collider.isTrigger == true)
            {
                continue;
            }

            // if we got here, we hit something, so we should NOT continue
            return false;
        }

        return true;
    }

    bool HasSafeGround()
    {
        Vector3 rayPos = new Vector3();

        rayPos.y = boxCollider.bounds.min.y;

        if (spriteRenderer.flipX == false)  // going right
        {
            rayPos.x = boxCollider.bounds.max.x;
        }
        else
        {
            rayPos.x = boxCollider.bounds.min.x;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, Vector2.down, 0.25f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == boxCollider)
            {
                // stop hitting yourself
                continue;
            }

            if (hit.collider.isTrigger)
                continue;

            // if we got here, we hit something, so we still have solid ground beneath us.
            return true;
        }

        return false;
    }

}
