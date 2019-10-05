using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void Start()
    {
        //Debug.Log("AttackHitbox::Start");
    }

    public int Damage = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("AttackHitbox::OnTriggerStay2D -- " + collision.gameObject.name);

        Health h = collision.GetComponentInParent<Health>();

        if (h == null)
            return;

        h.TakeDamage(Damage);
    }

}
