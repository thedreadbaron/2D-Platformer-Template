using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    GameObject Player;
    private RespawnController respawnController;
    private bool triggered = false;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        respawnController = Player.GetComponent<RespawnController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player && !triggered)
        {
            StartCoroutine(respawnController.DeathSequence());
            triggered = true;
            StartCoroutine(DeathDelay());
        }
    }

    public IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3.5f);
        triggered = false;
    }
}