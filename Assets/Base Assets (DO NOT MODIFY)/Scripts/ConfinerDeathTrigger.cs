using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerDeathTrigger : MonoBehaviour
{
    GameObject Player;
    private RespawnController respawnController;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        respawnController = Player.GetComponent<RespawnController>();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            StartCoroutine(respawnController.DeathSequence());
        }
    }
}
