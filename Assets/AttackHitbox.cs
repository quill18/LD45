using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AttackHitbox : MonoBehaviour
{
    private void Start()
    {
        //Debug.Log("AttackHitbox::Start");
    }

    public int Damage = 1;

    private void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log("AttackHitbox::OnTriggerStay2D -- " + collision.gameObject.name);

        Health h = collider.GetComponentInParent<Health>();

        if (h != null)
        {
            h.TakeDamage(Damage);
            return;
        }

        DestructibleTerrain dt = collider.GetComponentInParent<DestructibleTerrain>();

        if(dt != null)
        {
            //DestroyTile(collider, dt);
        }

    }

    void DestroyTile(Collider2D collider, DestructibleTerrain dt)
    {
        Debug.Log("DestroyTile -- " + dt.gameObject.name);

        Tilemap tilemap = dt.GetComponentInParent<Tilemap>();

        Vector3Int tilePos = tilemap.WorldToCell(collider.gameObject.transform.position);
        Debug.Log(tilePos);
        Debug.Log(tilemap.GetTile(tilePos));

        tilemap.SetTile(tilePos, null);
    }

}
