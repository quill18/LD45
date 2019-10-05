using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    Animator animator;

    BoxCollider2D boxCollider;

    Vector3 _velocity;

    public float WalkingSpeed = 4;

    bool isGrounded;

    float maxVelocity = 60; // world units per second

    float jumpHeight = 2.1f;

    public void SetXVelocity(float x)
    {
        _velocity.x = x;

    }

    public void DoJump()
    {
        if (isGrounded)
        {
            _velocity.y = Mathf.Sqrt(-2.0f * Physics2D.gravity.y * jumpHeight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Setup Movement
        _velocity.y += Physics2D.gravity.y * Time.deltaTime;


        // Apply Movement
        _velocity = Vector3.ClampMagnitude(_velocity, maxVelocity);
        transform.Translate(_velocity * Time.deltaTime);

        // Check for collision box overlaps
        Collider2D[] cols = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.bounds.size, 0);

        // Assume we aren't grounded unless we collide with something

        isGrounded = false;

        Debug.Log("---------------------------------------------" + cols.Length);

        float oldY = transform.position.y;

        foreach (Collider2D col in cols)
        {
            if (col == boxCollider)
            {
                // ourselves
                continue;
            }

            if (col.isTrigger == true)
            {
                continue;
            }
        

            ColliderDistance2D dist = col.Distance(boxCollider);

            if (dist.isOverlapped)
            {

                float a = Vector2.Angle(dist.normal, Vector2.up);
                Vector3 delta = dist.pointA - dist.pointB;
                transform.Translate(delta);

                // Are we touching ground?
                if (_velocity.y <= 0 && a < 45)
                {
                    //Debug.Log("grounded!");
                    isGrounded = true;
                    _velocity.y = 0f;
                }
                else if (_velocity.y > 0 && a > 135)
                {
                    //Debug.Log("roof!");
                    isGrounded = true;
                    _velocity.y = 0;
                }
            }
        }




        // Update Animations

        if (_velocity.x < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (_velocity.x > 0.1f)
            transform.localScale = new Vector3(1, 1, 1);

        animator.SetFloat("walkSpeed", Mathf.Abs(_velocity.x));
        animator.SetBool("isJumping", !isGrounded);
    }
}
