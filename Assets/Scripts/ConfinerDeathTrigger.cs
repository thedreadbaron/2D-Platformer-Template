using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerDeathTrigger : MonoBehaviour
{
    GameObject Player;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            StartCoroutine(Player.GetComponent<RespawnController>().DeathSequence());
        }
    }
}
