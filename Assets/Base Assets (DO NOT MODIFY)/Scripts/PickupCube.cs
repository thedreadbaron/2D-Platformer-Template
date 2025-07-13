using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCube : MonoBehaviour
{
    GameObject Player;
    private Inventory inventory;
    public GameObject pickupBurst;
    float Y;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        inventory = Player.GetComponent<Inventory>();
        Y = Random.Range(0,360);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Y += 100 * Time.deltaTime;
        transform.localEulerAngles = new Vector3 (0,Y,0);

        if (Y > 360.0f)
        {
            Y = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            inventory.PickupCountIncrease(1);
            inventory.pickupSound.Play();
            Instantiate(pickupBurst, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
