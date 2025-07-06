using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDash : MonoBehaviour
{
    GameObject Player;
    float Y;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Y = Random.Range(0,360);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Y += 50f * Time.deltaTime;
        transform.localEulerAngles = new Vector3 (55,Y,45);

        if (Y > 360.0f)
        {
            Y = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            Player.GetComponent<CharacterController2D>()._ToggleDash = true;
            Player.GetComponent<Inventory>().pickupSound.Play();
            Destroy(this.gameObject);
        }
    }
}
