using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }



    public GameObject[] AttackPrefabs; // Could be something like a fireball, or just an FX like a Woosh

    public BoxCollider2D[] HitBoxes; // Our attack hitbox. Optional, because we might spawn projectiles instead

    public Vector3 AttackPrefabOffset;

    Animator animator;
    SpriteRenderer spriteRenderer;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + AttackPrefabOffset, 0.1f);
    }

    protected void DoAttack()
    {
        animator.SetTrigger("isAttacking");

        SpawnPrefabs();
    }

    private void SpawnPrefabs()
    {
        if (AttackPrefabs == null)
        {
            // Nothing to spawn
            return;
        }

        foreach (GameObject AttackPrefab in AttackPrefabs)
        {
            Vector3 off = AttackPrefabOffset;

            if (spriteRenderer.flipX)
            {
                off.x = -off.x;
            }

            GameObject go = Instantiate(AttackPrefab, transform.position + off, Quaternion.identity);
            go.SetActive(true);

            SpriteRenderer sr = go.GetComponentInChildren<SpriteRenderer>();

            if (sr == null)
            {
                return;
            }

            sr.flipX = spriteRenderer.flipX;

        }
    }
}
