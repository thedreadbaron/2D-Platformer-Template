using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReveal : MonoBehaviour
{
    GameObject Player;
    public Animator animator;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            animator.SetBool("Reveal", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            animator.SetBool("Reveal", false);
        }
    }
}
