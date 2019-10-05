using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : Attacks
{
    float AttackCooldown = 0.25f;
    float timeLeft;

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0 && Input.GetButtonDown("Fire1") )
        {
            timeLeft = AttackCooldown;
            DoAttack();
        }
    }
}
