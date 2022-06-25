using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    GameObject Player;
    private CapsuleCollider2D capsule_collider;
    private BoxCollider2D box_collider;
    private GameObject target=null;
    private Vector3 offset;
    void Start()
    {
        target = null;
    }
    private void Awake()
    {
        capsule_collider = GetComponent<CapsuleCollider2D>();
        box_collider = GetComponent<BoxCollider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            target = col.gameObject;
            offset = target.transform.position - transform.position;
            if (col.GetComponent<PlayerMovement>().crouch)
                StartCoroutine(DropThrough());
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
    }
    void LateUpdate()
    {
        if (target != null) 
        {
            target.transform.position = transform.position+offset;
        }
    }

    IEnumerator DropThrough() 
	{
        target = null;
		capsule_collider.enabled = false;
        box_collider.enabled = false;
		yield return new WaitForSeconds(0.3f);
		capsule_collider.enabled = true;
        box_collider.enabled = true;
	}
}
