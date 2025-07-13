using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject Player;
    private bool triggered = false;
    private RespawnController respawnController;
    private Transform flagInactive;
    private Transform flagActive;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        respawnController = Player.GetComponent<RespawnController>();
        flagInactive = this.gameObject.transform.GetChild(0);
        flagActive = this.gameObject.transform.GetChild(1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player && !triggered)
        {
           respawnController.currentCheckpoint = transform;
           flagInactive.gameObject.SetActive(false);
           flagActive.gameObject.SetActive(true);
           triggered = true;
        }
    }
}
