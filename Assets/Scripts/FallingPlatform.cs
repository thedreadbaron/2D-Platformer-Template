using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    GameObject Player;
    private CapsuleCollider2D capsule_collider;
    private Animator animator;
    private Rigidbody2D rb;
    Vector3 startPos;
    Vector3 startRot;
    private bool triggered = false;

    public float shakeDuration = 0.64f;
    public float respawnDelay = 2f;

    private void Awake()
    {
        capsule_collider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject == Player && Player.GetComponent<PlayerMovement>().crouch && capsule_collider.enabled)
        {
                StartCoroutine(DropThrough());
                StartCoroutine(Player.GetComponent<CharacterController2D>().PlatFallDashCooldown());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.rigidbody.linearVelocity.y < 0f)
        {
                StartCoroutine(PlatFall());
                triggered = true;
        }
    }

    IEnumerator DropThrough() 
	{
		capsule_collider.enabled = false;
		yield return new WaitForSeconds(0.3f);
		capsule_collider.enabled = true;
	}

    IEnumerator PlatFall() 
	{
        animator.SetTrigger("Wiggle");
		yield return new WaitForSeconds(shakeDuration);
		rb.bodyType = RigidbodyType2D.Dynamic;
        capsule_collider.enabled = false;
        yield return new WaitForSeconds(respawnDelay);
        rb.bodyType = RigidbodyType2D.Static;
        transform.localPosition = startPos;
        transform.localEulerAngles = startRot;
        capsule_collider.enabled = true;
        triggered = false;
	}
}
