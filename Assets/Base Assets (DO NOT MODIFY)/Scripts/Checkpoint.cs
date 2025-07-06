using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject Player;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            Player.GetComponent<RespawnController>().currentCheckpoint = transform;
        }
    }
}
