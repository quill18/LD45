using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingRock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = Random.Range(-initialSpin, initialSpin);
    }
    Rigidbody2D rb;

    public GameObject ExplosionPrefab;
    float initialSpin = 360;
    int damage = 1;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health h = collision.gameObject.GetComponentInParent<Health>();
        DoDamage(h);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health h = collision.gameObject.GetComponentInParent<Health>();
        DoDamage(h);

        DestructibleTerrain dt = collision.gameObject.GetComponentInParent<DestructibleTerrain>();
        DestroyTerrain(dt, collision);

        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void DoDamage(Health h)
    {
        if (h == null)
            return;

        h.TakeDamage(damage);
    }

    void DestroyTerrain(DestructibleTerrain dt, Collision2D collision)
    {
        if (dt == null)
            return;

        Tilemap tilemap = dt.GetComponentInParent<Tilemap>();

        Vector2 pos = collision.contacts[0].point;

        Vector3Int tilePos = tilemap.WorldToCell(pos);
        if (tilemap.GetTile(tilePos) != null)
        {
            tilemap.SetTile(tilePos, null);

            if (dt.DebrisPrefab != null)
            {
                Instantiate(dt.DebrisPrefab, pos, Quaternion.identity);
            }

        }
    }
}
