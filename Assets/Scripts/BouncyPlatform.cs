using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    GameObject Player;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.velocity.y < 0.1f)
        {
            collision.rigidbody.AddForce(transform.up * 50f, ForceMode2D.Impulse);
            audioSource.pitch = (Random.Range(0.9f, 1.1f));
            audioSource.Play();
        }
    }
}
