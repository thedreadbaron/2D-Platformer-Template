using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraToggle : MonoBehaviour
{
    GameObject Player;
    public GameObject virtualCam;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        virtualCam.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.gameObject == Player)
        {
            virtualCam.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {   
        if (col.gameObject == Player)
        {
            virtualCam.SetActive(false);
        }
    }
}
