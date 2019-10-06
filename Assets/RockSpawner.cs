using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject[] Rocks;
    PlayerPlatformerController player;

    float rockCooldown = 1;
    float nextRock = 0;

    float rockXVel = 3;
    float xVariation = 4;

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            // Might have died
            player = GameObject.FindObjectOfType<PlayerPlatformerController>();
            return;
        }

        nextRock -= Time.deltaTime;

        if(nextRock <= 0)
        {
            nextRock = Random.Range(rockCooldown * 0.5f, rockCooldown * 1.5f);

            Vector2 pos = player.transform.position;
            pos.x += Random.Range(-xVariation*2f, xVariation);
            pos.y += 10f; // Would be smarter to check camera range.
            GameObject go = Instantiate(Rocks[Random.Range(0, Rocks.Length)], pos, Quaternion.identity);

            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

            rb.velocity = new Vector2( Random.Range(rockXVel*0.5f, rockXVel*1.5f), 0);

        }
    }
}
