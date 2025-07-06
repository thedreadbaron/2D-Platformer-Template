using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    GameObject Player;
    private CapsuleCollider2D capsule_collider;

    private void Awake()
    {
        capsule_collider = GetComponent<CapsuleCollider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject == Player && Player.GetComponent<PlayerMovement>().crouch && capsule_collider.enabled)
        {
                StartCoroutine(DropThrough());
                StartCoroutine(Player.GetComponent<CharacterController2D>().PlatFallDashCooldown());
        }
    }

    IEnumerator DropThrough() 
	{
		capsule_collider.enabled = false;
		yield return new WaitForSeconds(0.3f);
		capsule_collider.enabled = true;
	}
}
