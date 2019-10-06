using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public int HPs = 1;
    public bool GodMode = false;

    public float InvulTime = 0.1f;
    float invulTimeLeft;

    public float FlinchTime = 0f;
    float flinchTimeLeft;

    public GameObject[] DeathPrefabs;

    public AudioClip[] DeathSounds;
    public AudioClip[] HurtSounds;

    private void Update()
    {
        invulTimeLeft -= Time.deltaTime;
        flinchTimeLeft -= Time.deltaTime;
    }

    public bool IsInvulnerable()
    {
        return invulTimeLeft > 0;
    }

    public bool IsFlinching()
    {
        return flinchTimeLeft > 0;
    }

    public void TakeDamage(int amt)
    {
        if (invulTimeLeft > 0)
            return;

        HPs -= amt;

        invulTimeLeft = InvulTime;
        flinchTimeLeft = FlinchTime;

        if (HPs <= 0)
        {
            Die();
        }
        else
        {
            SoundManager.PlayClip(HurtSounds);
        }

        PhysicsObject po = GetComponent<PhysicsObject>();
        if (po != null)
            po.StopDash();
    }

    public void Die()
    {
        if (GodMode)
        {
            Debug.Log("GOD MODE SAVE!");
            return;
        }

        // Spawn death animation
        if(DeathPrefabs != null)
        {
            foreach(GameObject dp in DeathPrefabs)
            {
                Instantiate(dp, transform.position, Quaternion.identity);
            }
        }

        SoundManager.PlayClip(DeathSounds);

        Destroy(gameObject);
    }

}
