using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructAfterAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = this.GetComponentInChildren<Animator>();
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        Destroy(gameObject, info.length + Delay);
    }

    public float Delay = 0;
}
