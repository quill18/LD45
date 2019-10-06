using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioClip[] Sounds;
    public LayerMask LayerMask;
    float radius = 3f;
    float SoundCooldown = 2f;
    float cooldown;

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.PlayClip(Sounds);
    }*/

    private void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown > 0)
            return;

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask);
        
        if(cols != null && cols.Length > 0)
        {
            Debug.Log("TriggerSound: " + cols[0].gameObject.name);
            SoundManager.PlayClip(Sounds);
            cooldown = Random.Range(SoundCooldown * .5f, SoundCooldown * 1.5f);
        }

    }
}
