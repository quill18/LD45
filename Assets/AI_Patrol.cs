using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        charCon = GetComponent<CharacterController2D>();
    }

    CharacterController2D charCon;
    BoxCollider2D boxCollider;

    // Update is called once per frame
    void Update()
    {
        if(IsSafeToAdvance() == false)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }

        charCon.SetXVelocity(transform.localScale.x * charCon.WalkingSpeed);
    }

    bool IsSafeToAdvance()
    {
        return HasSafeGround() && HasSpaceAhead();
    }

    bool HasSpaceAhead()
    {
        Vector3 rayPos = new Vector3();

        rayPos.y = boxCollider.bounds.center.y;

        if (transform.localScale.x > 0)  // going right
        {
            rayPos.x = boxCollider.bounds.max.x;
        }
        else
        {
            rayPos.x = boxCollider.bounds.min.x;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, 
            (transform.localScale.x > 0) ? Vector2.right : Vector2.left, 
            0.1f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == boxCollider)
            {
                // stop hitting yourself
                continue;
            }

            if(hit.collider.isTrigger == true)
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

        if (transform.localScale.x > 0)  // going right
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

            // if we got here, we hit something, so we still have solid ground beneath us.
            return true;
        }

        return false;
    }
}
