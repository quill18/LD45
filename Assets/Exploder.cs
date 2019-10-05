using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NumSpawn; i++)
        {
            GameObject p = Prefabs[ Random.Range(0, Prefabs.Length) ];

            Vector2 off = Random.insideUnitCircle * SpawnRadius;
            if (UpSpawnOnly)
                off.y = Mathf.Abs(off.y);

            GameObject go = Instantiate(p,
                transform.position + (Vector3)off,
                Quaternion.Euler(0, 0, Random.Range(0f, 360f))
                );

            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

            Vector2 force = off.normalized * Random.Range(ExplosionForce / 2f, ExplosionForce * 1.5f);

            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public GameObject[] Prefabs;
    public int NumSpawn = 100;
    public float ExplosionForce = 10f; // Actual force will be +/- 50%
    public float SpawnRadius = 0.1f;
    public bool UpSpawnOnly = true;

    // Update is called once per frame
    void Update()
    {
        
    }
}
