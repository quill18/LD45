using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    public float WallDestRange=0;
    public float WallDestHeight=0;

    public Vector3 AttackPrefabOffset;

    Animator animator;
    SpriteRenderer spriteRenderer;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + AttackPrefabOffset, 0.1f);
    }

    protected void DoAttack( float windUpTime = 0f )
    {
        animator.SetTrigger("isAttacking");
        StartCoroutine(CO_DoAttack(windUpTime));
    }

    IEnumerator CO_DoAttack( float windUpTime )
    {
        if(windUpTime > 0)
            yield return new WaitForSeconds(windUpTime);

        SpawnPrefabs();
        if (WallDestHeight > 0 && WallDestRange > 0)
            StartCoroutine(WallDestruction());
    }

    IEnumerator WallDestruction()
    {
        Vector2 pos = transform.position + (spriteRenderer.flipX ? -AttackPrefabOffset : AttackPrefabOffset);
        Vector2 dir = spriteRenderer.flipX ? Vector2.left : Vector2.right;

        bool tryAgain = false;

        do
        {
            tryAgain = false;

            RaycastHit2D[] hits = Physics2D.BoxCastAll(pos, new Vector2(1.0f, WallDestHeight), 0, dir);
            //Debug.Log("LENGTH: " + hits.Length);

            foreach (RaycastHit2D hit in hits)
            {
                //Debug.Log("HIT: " + hit.collider.name);

                DestructibleTerrain dt = hit.collider.GetComponentInParent<DestructibleTerrain>();
                if (dt == null)
                    continue;

                Tilemap tilemap = dt.GetComponentInParent<Tilemap>();

                Vector3Int tilePos = tilemap.WorldToCell(hit.point);
                if (tilemap.GetTile(tilePos) != null)
                {
                    tilemap.SetTile(tilePos, null);
                    // Because terrain is a single collider, we need to 
                    // repeat the cast to see if any other tiles would be hit.
                    tryAgain = true;
                    yield return null; // wait a frame
                }
            }
        } while (tryAgain);
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
